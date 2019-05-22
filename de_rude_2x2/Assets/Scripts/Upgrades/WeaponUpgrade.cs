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
    public string text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weapon.GetComponent<Gun>().damage = damage;
            weapon.GetComponent<Gun>().max_clip = max_clip;
            weapon.GetComponent<Gun>().current_clip = max_clip;
            weapon.GetComponent<Gun>().current_ammo = current_ammo;
            infoPanel.GetComponent<InfoPanel>().setInfoText(text);
            other.GetComponentInChildren<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
