using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Gun : MonoBehaviour
{
    public string gun_name = "Gun";
    public float damage = 100;
    public float fire_rate = 100;
    public float reload_time = 1;
    public float max_ammo = 150;
    public float max_clip = 30;
    public float current_clip = 0;
    public GameObject projectile;
    public bool unlocked = true;
    public Transform projectile_spawn_point;
    private bool can_shoot = true;

    abstract public void shoot();
    abstract public void reload();
}
