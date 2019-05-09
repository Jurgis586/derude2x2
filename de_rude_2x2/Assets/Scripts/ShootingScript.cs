using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public float fire_rate = 0.5f;
    public int damage = 1;
    public float projectile_speed = 50f;

    public GameObject bullet_prefab;

    private float next_fire_time = 0;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Debug.Log(cam);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > next_fire_time)
        {
            next_fire_time = Time.time + fire_rate;
            Debug.Log("shoot");
            Instantiate(bullet_prefab, transform.position, transform.rotation);
        }
    }
}
