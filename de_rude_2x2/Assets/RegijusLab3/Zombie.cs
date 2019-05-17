using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    // For movement
    private NavMeshAgent zombie;

    // For audio
    public AudioSource damage;
    public AudioSource death;

    // To calculate the score
    public GameObject Player;
    private int health;
    
    void Start()
    {
        zombie = gameObject.GetComponent<NavMeshAgent>();
        health = 3;
    }

    void Update()
    {
        zombie.SetDestination(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            Player.GetComponent<PlayerController>().changeScore(10);
            takeDamage();
        }
    }

    void takeDamage() {
        health--;
        if (health != 0) {
            damage.Play();
        } else {
            death.Play();
            Player.GetComponent<PlayerController>().changeScore(100);
            Destroy(gameObject);
        }
    }
}
