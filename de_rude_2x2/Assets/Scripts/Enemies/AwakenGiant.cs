using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakenGiant : MonoBehaviour
{
    public GameObject giant;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            giant.GetComponent<Giant>().makeAttacking(true);
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }
}
