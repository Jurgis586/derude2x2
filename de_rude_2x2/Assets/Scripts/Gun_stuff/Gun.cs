using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public string gun_name = "Gun";
    public float accuracy = 1;
    public float projectile_speed = 200f;
    public float damage = 100;
    public float fire_rate = 100;
    public float reload_time = 1;
    public float max_ammo = 150;
    public float current_ammo = 150;
    public float max_clip = 30;
    public float current_clip = 0;
    public GameObject projectile;
    public bool unlocked = true;
    public Transform projectile_spawn_point;
    public bool can_shoot = true;
    protected LayerMask mask = -1029;
    abstract public void Shoot();
    abstract public void Reload();


    public void Hide_Gun()
    {
        // if the gun belongs to layer, give a mask that hits enemies
        if(transform.tag == "Player_gun")
        {
            mask = ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Ignore Raycast")); // ignore both layerX and layerY
        }
        else
        {
            mask = ~(1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("Ignore Raycast")); // ignore both layerX and layerY
        }

        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>(true);
        renderer.enabled = false;
    }
    public void Show_Gun()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>(true);
        renderer.enabled = true;
    }
}
