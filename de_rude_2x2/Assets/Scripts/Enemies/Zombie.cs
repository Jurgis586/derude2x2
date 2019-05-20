using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : Enemy
{
    // For movement
    private NavMeshAgent agent;
    private Transform player_collider;
    private Vector3 m_EulerAngleVelocity = new Vector3(0, 100, 0);
    private Rigidbody rb;
    private Animator anim;

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
    }

    void Update()
    {
        if (alive && agent.enabled)
            agent.SetDestination(player_collider.transform.position);
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
