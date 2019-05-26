using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazookaUnlock : MonoBehaviour
{
    public GameObject weapon;
    public GameObject infoPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weapon.GetComponent<Gun>().unlocked = true;
            infoPanel.GetComponent<InfoPanel>().setInfoText("You've unlocked a rocket launcher!");
            other.GetComponentInChildren<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
