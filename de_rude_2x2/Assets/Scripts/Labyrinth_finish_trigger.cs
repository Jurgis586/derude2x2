using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth_finish_trigger : MonoBehaviour
{
    bool cleared;
    public GameObject platform;
    Collider col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        cleared = false; //load from profile on build
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered");
        if (other.tag == "Player")
        {
            Debug.Log("is player");
            cleared = true;
            platform.SetActive(true);
        }
    }
}
