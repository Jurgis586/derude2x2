using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    public float speed = 10;
    public float lifetime = 5;
    private int layerMask;
    private Rigidbody rb;
    private Vector3 offset;
    private float raycast_range;
    private float range_left = 1000;
    private float damage = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, transform.localScale.z/2);
        layerMask = 1 << 2; // = "ignore raycast" layer
        layerMask = ~layerMask; // raycast against everything BUT that layer
        rb = GetComponent<Rigidbody>();
    }

    public void init(float damage)
    {
        this.damage = damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(range_left >= 0)
        {
            Vector3 pos = transform.position - offset;
            raycast_range = 1 + speed * Time.deltaTime;
            if (raycast_range < 0)
                raycast_range = 1;
            rb.velocity = transform.forward * speed;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(pos, transform.TransformDirection(Vector3.forward), out hit, raycast_range, layerMask))
            {
                Debug.DrawRay(pos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 10);
                //Debug.Log("Did Hit: " + hit.transform.tag + " range: " + raycast_range + " start: " + pos);
                if(hit.transform.tag == "Enemy")
                {
                    //do damage
                    enemy_script enemy = hit.collider.GetComponent<enemy_script>();
                    if(enemy)
                        enemy.get_damage(damage);
                }
                Destroy(gameObject);
            }
            else
            {
                Debug.DrawRay(pos, transform.TransformDirection(Vector3.forward) * raycast_range, Color.white);
                //Debug.Log("Did not Hit");
            }
            range_left -= (raycast_range - 1);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
