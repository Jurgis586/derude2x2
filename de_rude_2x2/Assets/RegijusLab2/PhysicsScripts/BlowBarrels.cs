using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBarrels : MonoBehaviour
{
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.position += Vector3.forward;
        }
    }
}
