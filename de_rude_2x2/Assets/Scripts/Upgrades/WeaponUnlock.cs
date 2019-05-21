using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    public GameObject weapon;
    public GameObject infoPanel;

    private void OnTriggerEnter(Collider other)
    {
        weapon.GetComponent<Gun>().unlocked = true;
        infoPanel.GetComponent<InfoPanel>().setInfoText("You've unlocked a space gun!");
        Destroy(gameObject);
    }
}
