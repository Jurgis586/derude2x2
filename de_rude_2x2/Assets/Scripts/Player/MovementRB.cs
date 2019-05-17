using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementRB : MonoBehaviour
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool _isGrounded = false;
    public float GroundDistance = 0.2f;
    private Rigidbody rb;
    private CapsuleCollider col;


    public float airdrag = 0.2f;
    public float normaldrag;
    public float sprintSpeedMult = 1.4f;

    private float dToGround;

    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
        col = GetComponentInChildren<CapsuleCollider>();
        dToGround = transform.localScale.y * (col.height / 2) + GroundDistance;
        rb.freezeRotation = true;
        rb.useGravity = true;
        normaldrag = rb.drag;
    }

    void FixedUpdate()
    {
        _isGrounded = IsGrounded();
        if (_isGrounded)
        {
            //print("grounded");
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            targetVelocity = transform.TransformDirection(targetVelocity);
            if (Input.GetKey(KeyCode.LeftShift))
                targetVelocity *= speed*sprintSpeedMult;
            else
                targetVelocity *= speed;
            

            // Apply a force that attempts to reach our target velocity
            Vector3 velocity = rb.velocity;
            Vector3 velocityChange = (targetVelocity - velocity);
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            // Jump
            if (_isGrounded && Input.GetButton("Jump"))
            {
                rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                _isGrounded = false;
                rb.drag = airdrag;
            }
        }

        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * rb.mass * 2, 0));

        _isGrounded = false;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    bool IsGrounded()
    {
        //should do multiple raycasts so you dont get stuck on holes or a collision check
        if(Physics.Raycast(transform.position, -Vector3.up, dToGround + 0.1f))
        {
            //print("grounded");
            return true;
        }
        else
        {
            //print("not grounded");
            //print(dToGround);
            return false;
        }
    }
}
