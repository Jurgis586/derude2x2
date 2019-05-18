using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestTrigger : MonoBehaviour
{
    public GameObject player;
    public GameObject treasureChest;
    public GameObject treasureChestPickup;
    private float inactiveTime;

    private void Start()
    {
        treasureChestPickup.SetActive(false);
        inactiveTime = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.time - inactiveTime > 30)
        {
            inactiveTime = Time.time;
            treasureChestPickup.SetActive(true);
        }
    }
}
