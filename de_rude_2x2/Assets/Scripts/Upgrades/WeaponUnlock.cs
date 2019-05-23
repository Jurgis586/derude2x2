using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUnlock : MonoBehaviour
{
    public GameObject weapon;
    public GameObject infoPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            weapon.GetComponent<Gun>().unlocked = true;
            infoPanel.GetComponent<InfoPanel>().setInfoText("You've unlocked a space gun! Now you can enter the Arena");
            other.GetComponentInChildren<AudioSource>().Play();
            StartCoroutine(cooldown());
            Destroy(gameObject);
        }
    }

    IEnumerator cooldown()
    {
        yield return new WaitForSeconds(3);
    }
}
