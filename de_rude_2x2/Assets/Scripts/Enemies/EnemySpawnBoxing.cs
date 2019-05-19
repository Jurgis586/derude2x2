using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBoxing : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    private float repeatRate = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger active boxing");
        if (other.gameObject.tag == "Player")
        {
            enemy.SetActive(true);
            Debug.Log("trigger active in if boxing");
            InvokeRepeating("EnemySpawner", 0.5f, repeatRate);
            Destroy(gameObject, 11);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }


    void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position, enemyPos.rotation);
    }
}
