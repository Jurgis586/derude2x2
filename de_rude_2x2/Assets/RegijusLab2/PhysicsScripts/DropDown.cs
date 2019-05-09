using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDown : MonoBehaviour
{
    public Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) {
        if (rb.useGravity == false) {
            rb.useGravity = true;
        }
    }
}
