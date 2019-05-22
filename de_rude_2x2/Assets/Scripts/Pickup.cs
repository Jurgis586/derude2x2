using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float spin_speed = 50;
    public Vector3 direction = new Vector3(0, 1, 0);
    public float value;
    public string type;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (direction, spin_speed * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Debug.Log("player pickup");
            other.GetComponentInChildren<PlayerController>().apply_effect(type, value);
            other.GetComponentInChildren<AudioSource>().Play();
            Destroy(gameObject);
        }
    }
}
