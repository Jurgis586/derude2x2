using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving_platform_up : MonoBehaviour
{
    public float accelerationmult = 0.2f;
    public float maxspeed = 5;
    public float distance = 5;
    public float stop_time = 2;
    public bool one_way = false;

    public float speed;
    private float elapsed_time;
    private float distance_traveled;
    private bool moving = false;
    private float location_start;
    private float location_end;
    private bool up = true;
    // Start is called before the first frame update
    void Start()
    {
        elapsed_time = 0;
        distance_traveled = 0;
        location_start = transform.position.y;
        location_end = location_start + distance;
        speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float time = Time.deltaTime;
        if (Mathf.Abs(speed) < Mathf.Abs(maxspeed))
        {
            speed += maxspeed * accelerationmult;
            if (Mathf.Abs(speed) > Mathf.Abs(maxspeed))
                speed = maxspeed;
        }
        if (moving)
            if(distance_traveled >= distance)
            {
                distance_traveled = 0;
                moving = false;
                maxspeed *= -1;
                if(up)
                {
                    transform.position = new Vector3(transform.position.x, location_end, transform.position.z);
                    up = false;
                    speed = -1;
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, location_start, transform.position.z);
                    up = true;
                    speed = 1;
                }
                if (one_way)
                    //gameObject.SetActive(false);
                    this.enabled = false;
            }
            else
            {
                transform.Translate(Vector3.up * time * speed);
                distance_traveled += time * Mathf.Abs(speed); 
            }
        else
            {
                if (elapsed_time >= stop_time)
                {
                    elapsed_time = 0;
                    moving = true;
                }
                else
                    elapsed_time += time;
            }
    }
}
