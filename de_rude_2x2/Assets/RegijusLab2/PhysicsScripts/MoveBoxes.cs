using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBoxes : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other) {
        rb.AddForce(Vector3.back, ForceMode.Force);
    }
}
