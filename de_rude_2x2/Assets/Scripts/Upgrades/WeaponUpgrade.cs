using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public GameObject weapon;
    public int damage;
    public int max_clip;
    public int current_ammo;

    public GameObject infoPanel;

    private void OnTriggerEnter(Collider other)
    {
        weapon.GetComponent<Gun>().damage = damage;
        weapon.GetComponent<Gun>().max_clip = max_clip;
        weapon.GetComponent<Gun>().current_clip = max_clip;
        weapon.GetComponent<Gun>().current_ammo = current_ammo;
        infoPanel.GetComponent<InfoPanel>().setInfoText("You've picked up a pistol upgrade!");
        Destroy(gameObject);
    }
}
