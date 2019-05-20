using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockWeapon : MonoBehaviour
{
    public GameObject weapon;

    private void OnCollisionEnter(Collision collision)
    {
        weapon.GetComponent<Rifle>().unlocked = true;
    }
}
