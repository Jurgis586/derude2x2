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
    protected bool can_shoot = true;

    abstract public void Shoot();
    abstract public void Reload();

    public void Hide_Gun()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>(true);
        renderer.enabled = false;
    }
    public void Show_Gun()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>(true);
        renderer.enabled = true;
    }
}
