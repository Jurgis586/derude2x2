using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rocket : MonoBehaviour
{
    public float explosion_power = 10.0F;
    public float explosion_radius = 3;
    public float speed = 10;
    private int layerMask;
    private Rigidbody rb;
    private float raycast_range;
    private float range_left = 1000;
    private float damage = 1;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    public void init(float damage, float speed, float explosion_radius, float explosion_power, int layerMask = 2)
    {
        this.damage = damage;
        this.speed = speed;
        this.explosion_radius = explosion_radius;
        this.explosion_power = explosion_power;
        this.layerMask = layerMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (range_left >= 0)
        {
            Vector3 pos = transform.position - (transform.forward * transform.localScale.z);
            raycast_range = 1 + speed * Time.deltaTime * 1.2f;
            if (raycast_range < 0)
                raycast_range = 1;
            rb.velocity = transform.forward * speed;
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            //if (Physics.Raycast(pos, transform.forward, out hit, raycast_range * 1.2f, layerMask))
            if (Physics.Raycast(previousPosition, (transform.position - previousPosition), out hit, Vector3.Distance(transform.position, previousPosition) * 1.2f, layerMask))
            {
                Debug.DrawRay(previousPosition, (transform.position - previousPosition), Color.yellow, 10);
                //Debug.Log("Did Hit: " + hit.transform.tag + " range: " + raycast_range + " start: " + pos);
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, explosion_radius);

                /*var mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                mySphere.transform.localScale = new Vector3(explosion_radius, explosion_radius, explosion_radius);
                mySphere.transform.position = explosionPos;*/

                foreach (Collider obj in colliders)
                {


                    Rigidbody rb = obj.GetComponentInChildren<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(explosion_power, explosionPos, explosion_radius, 3.0F);

                    if (obj.transform.tag == "Enemy")
                    {
                        obj.GetComponentInChildren<NavMeshAgent>().enabled = false;
                        Debug.Log("tag: " + hit.transform.tag);
                        //do damage
                        Enemy enemy = obj.GetComponentInChildren<Enemy>();
                        if (enemy)
                            enemy.receive_damage(damage);
                        else
                            Debug.Log("ENEMY NOT FOUND");
                    }
                    else if (obj.transform.tag == "Player")
                    {
                        obj.GetComponentInChildren<PlayerController>().changeHealthBy(-damage/10f);
                        Debug.Log("tag: " + hit.transform.tag);
                    }
                    else
                    {
                        Debug.Log("tag: " + hit.transform.tag);
                    }

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
