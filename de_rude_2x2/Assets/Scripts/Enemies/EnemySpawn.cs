using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    private float repeatRate = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger active");
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("trigger active in if");
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
