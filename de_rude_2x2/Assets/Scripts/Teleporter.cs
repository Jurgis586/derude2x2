using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public CapsuleCollider player;
    public GameObject location;
    public GameObject arenaZombie;
    public GameObject infoPanel;

    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(arenaZombie);
        if (arenaZombie == null || !arenaZombie.gameObject.GetComponent<ArenaZombie>().alive)
        {
            if (collision.transform.tag == "Player")
                player.transform.position = location.transform.position;
        }
        else
        {
            infoPanel.gameObject.GetComponent<InfoPanel>()
                .setInfoText(@"To leave arena, you must defeat Arena Zombie first.");
        }
    }
}
