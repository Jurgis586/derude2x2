using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTableDown : MonoBehaviour
{
    public Rigidbody table;

    void Start() {
        table = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider collider) {
        table.useGravity = true;
        table.mass = 25;
    }
}
