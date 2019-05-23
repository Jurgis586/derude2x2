using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterToArenaAfterQuestScript : MonoBehaviour
{
    public CapsuleCollider player;
    public GameObject location;
    public GameObject spaceGun;
    public GameObject infoPanel;

    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<CapsuleCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (spaceGun.gameObject.GetComponent<SpaceGun>().unlocked)
        {
            Debug.Log("unlocked");
            if (collision.transform.tag == "Player")
                player.transform.position = location.transform.position;
        }
        else
        {
            Debug.Log("locked");
            infoPanel.gameObject.GetComponent<InfoPanel>().setInfoText(@"To enter arena you have to unlock Space Gun.
                For more information go see Knight of Arena at the entrance.");
        }

    }
}
