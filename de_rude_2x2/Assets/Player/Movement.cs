using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 7.0f;
    public float sprintspeed = 2;
    public float jumpSpeed = 6.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        sprintspeed *= speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            if(Input.GetKey(KeyCode.LeftShift))
                moveDirection *= sprintspeed;
            else
                moveDirection *= speed;

            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        print(moveDirection);
        controller.Move(moveDirection * Time.deltaTime);
    }
}
