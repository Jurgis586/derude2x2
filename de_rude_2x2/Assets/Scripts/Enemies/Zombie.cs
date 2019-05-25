using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    // For movement
    public float change_path_period = 0f;
    private float change_path_time_left = 0f;
    private NavMeshAgent agent;
    private Transform player_collider;
    private Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
    private Animator anim;
    public bool attacking;

    // For audio
    public AudioSource damageSound;
    public AudioSource deathSound;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        hp = hp_max;
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = move_speed;
        player_collider = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().transform;
        anim = GetComponent<Animator>();
        if (attacking)
        {
            anim.Play("walk");
        }
    }

    void Update()
    {
        if (alive && agent.enabled)
        {
            if (change_path_time_left < 0)
            {
                change_path_time_left = change_path_period;
                agent.SetDestination(player_collider.transform.position);
            }
            else
            {
                change_path_time_left -= Time.deltaTime;
            }
        }
    }

    public void makeAttacking(bool type)
    {
        if (type)
        {
            attacking = true;
            anim.Play("walk");
        }
        else
        {
            attacking = false;
            anim.Play("idle");
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

        if (hp <= 0 && alive)
        {
            player_collider.GetComponent<PlayerController>().changeScore(score);
            die();
        }
        else if (alive)
        {
            damageSound.Play();
        }
    }

    public override void die()
    {
        alive = false;
        rb.velocity = agent.velocity;
        agent.enabled = false;
        deathSound.Play();
        anim.Play("fallingback");
        transform.gameObject.tag = "Untagged";
        StartCoroutine(remove());
    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
