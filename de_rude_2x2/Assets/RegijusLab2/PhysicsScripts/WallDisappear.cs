using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDisappear : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null) {
            Destroy(other.gameObject, 3f);
        }
    }
}
