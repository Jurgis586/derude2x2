using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaKnightScript : MonoBehaviour
{
    public GameObject knight;
    public GameObject infoPanel;
    public CapsuleCollider playerBody;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Player")
            if (Input.GetKey(KeyCode.F))
                infoPanel.gameObject.GetComponent<InfoPanel>().setInfoText(@"To enter the arena you have to unlock space gun which is located at the desert well.
                        But be careful, those zombieheads will attack you as soon as they will smell you.");
            else
                infoPanel.gameObject.GetComponent<InfoPanel>().setInfoText(@"Hold F button to speak.");
    }
}

