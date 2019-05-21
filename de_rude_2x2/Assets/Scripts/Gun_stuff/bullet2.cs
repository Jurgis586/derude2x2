using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet2 : MonoBehaviour
{
    public float speed = 10;
    private int layerMask;
    private Rigidbody rb;
    private float raycast_range;
    private float range_left = 1000;
    public float damage = 1;
    private Vector3 previousPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    public void init(float damage, float speed, int layerMask = 2)
    {
        this.damage = damage;
        this.speed = speed;
        /*this.layerMask = 1 << layerMask; // = "ignore raycast" layer
        this.layerMask = ~this.layerMask; // raycast against everything BUT that layer*/
        this.layerMask = layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        if(range_left >= 0)
        {
            Vector3 pos = transform.position - (transform.forward * transform.localScale.z);
            raycast_range = 1 + speed * Time.deltaTime * 1.2f;
            if (raycast_range < 0)
                raycast_range = 1;
            rb.velocity = transform.forward * speed;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            //if (Physics.Raycast(pos, transform.forward, out hit, raycast_range * 1.2f, layerMask))
            if (Physics.Raycast(previousPosition, (transform.position - previousPosition), out hit, Vector3.Distance(transform.position, previousPosition)*1.2f, layerMask))
            {
                Debug.DrawRay(pos, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow, 10);
                //Debug.Log("Did Hit: " + hit.transform.tag + " range: " + raycast_range + " start: " + pos);
                if(hit.transform.tag == "Enemy")
                {
                    //do damage
                    Enemy enemy = hit.collider.GetComponentInChildren<Enemy>();
                    if (enemy)
                        enemy.receive_damage(damage);
                    else
                        Debug.Log("ENEMY NOT FOUND");
                }
                else if(hit.transform.tag == "Player")
                {
                    hit.transform.GetComponentInChildren<PlayerController>().decreaseLife();
                }
                else
                {
                    //Debug.Log("tag: " + hit.transform.tag);
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
