using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenEnemy : MonoBehaviour
{
    public GameObject enemy;

    private void OnCollisionEnter(Collision collision)
    {
        enemy.SetActive(true);
    }
}
