using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public CapsuleCollider player;
    public GameObject location;

    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
            player.transform.position = location.transform.position;
    }
}
