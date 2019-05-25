using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZombies : MonoBehaviour
{
    // Locations
    public GameObject location1;
    public GameObject location2;

    public GameObject objectToSpawn;
    public float spawnTime;
    public int maxZombiesCount;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnZombie", spawnTime, spawnTime);
    }

    private void SpawnZombie()
    {
        int currentZombieCount = GameObject.FindGameObjectsWithTag("EnemyToClone").Length;
        if (currentZombieCount < maxZombiesCount)
        {
            Instantiate(objectToSpawn, location1.transform.position, Quaternion.identity);
            Instantiate(objectToSpawn, location2.transform.position, Quaternion.identity);
        }
    }
}
