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
        health = 300;
    }

    void Update()
    {
        zombie.SetDestination(GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<CapsuleCollider>().transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) {
            takeDamage();
        }
    }

    void takeDamage() {
        health--;
        if (health != 0) {
            damage.Play();
        } else {
            Player.GetComponent<PlayerController>().changeScore(1000);
            StartCoroutine(die());
        }
    }

    IEnumerator die()
    {
        death.Play();
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
