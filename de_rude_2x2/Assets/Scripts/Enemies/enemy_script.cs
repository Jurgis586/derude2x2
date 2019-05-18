using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy_script : Enemy
{
    private NavMeshAgent agent;
    private Transform player_collider;
    private Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        hp = hp_max;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = move_speed;
        player_collider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            //get_damage(0.5f);
            if (agent.enabled)
            {
                agent.SetDestination(player_collider.transform.position);
                Debug.DrawRay(player_collider.transform.position, Vector3.up * 2000, Color.magenta);
            }
        }
    }

    public override void receive_damage(float damage, string type = "flat")
    {
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
        if (hp <= 0)
            die();
    }

    public override void die()
    {
        alive = false;
        rb.velocity = agent.velocity;
        agent.enabled = false;
    }
}
