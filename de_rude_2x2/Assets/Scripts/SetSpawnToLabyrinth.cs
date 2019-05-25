using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnToLabyrinth : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerPrefs.SetString("StartSpawn", "Labyrinth");
        }
    }
}
