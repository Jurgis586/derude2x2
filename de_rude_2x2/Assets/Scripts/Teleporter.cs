using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject player;
    public GameObject location;

    private void OnCollisionEnter(Collision collision)
    {
        player.transform.position = location.transform.position;
    }
}
