using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaKnightScript : MonoBehaviour
{
    public GameObject knight;
    public GameObject infoPanel;
    bool askedOnce = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (Input.GetKey("F"))
            {
                if (!askedOnce)
                {
                    infoPanel.gameObject.GetComponent<InfoPanel>().setInfoText(@"HAHA! So you want to enter the Arena.
                        Good luck boy..");
                    askedOnce = true;
                }
                else
                {
                    infoPanel.gameObject.GetComponent<InfoPanel>().setInfoText(@"Alright, alright. You can find your space gun at the desert well.
                        But be careful, those zombieheads will attack you as soon as they will smell you.");
                }
            }
            else
            {
                infoPanel.gameObject.GetComponent<InfoPanel>().setInfoText(@"Hold F button to speak.");
            }

        }
    }
}

