﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret_script : Enemy
{
    public float turn_speed = 5f;
    public float shooting_angle = 10f;
    private Transform player_collider;
    private Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
    private Rigidbody rb;
    private Gun gun_script;
    private float next_fire_time = 0;

    // Start is called before the first frame update
    void Start()
    {
        alive = false;
        rb = gameObject.GetComponent<Rigidbody>();
        hp = hp_max;
        player_collider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().transform;
        gun_script = GetComponentInChildren<Gun>();
        gun_script.Reload();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            Vector3 dir = player_collider.transform.position - transform.position;
            float angle = Vector3.Angle(dir, transform.forward);
            if (Time.time > next_fire_time && angle < shooting_angle)
            {
                next_fire_time = Time.time + gun_script.fire_rate;

                gun_script.Shoot();
            }

            // The step size is equal to speed times frame time.
            float step = turn_speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    public override void receive_damage(float damage, string type = "flat")
    {
        Debug.Log("received_damage");
        switch (type)
        {
            // percent from current hp
            case "per_cur":
                hp = hp / 100 * damage;
                break;

            // percent from max hp
            case "per_max":
                hp -= hp_max / 100 * damage;
                break;

            // flat damage to hp
            default:
                hp -= damage;
                break;
        }
        if (alive && hp <= 0)
            die();
    }

    public override void die()
    {
        alive = false;
    }
}
