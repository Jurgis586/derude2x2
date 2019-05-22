using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertWellAttacking : MonoBehaviour
{
    public GameObject zombie1;
    public GameObject zombie2;
    public GameObject zombie3;
    public GameObject zombie4;
    public GameObject zombie5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            zombie1.GetComponent<Zombie>().makeAttacking(true);
            zombie2.GetComponent<Zombie>().makeAttacking(true);
            zombie3.GetComponent<Zombie>().makeAttacking(true);
            zombie4.GetComponent<Zombie>().makeAttacking(true);
            zombie5.GetComponent<Zombie>().makeAttacking(true);
            Destroy(gameObject);
        }
    }
}
