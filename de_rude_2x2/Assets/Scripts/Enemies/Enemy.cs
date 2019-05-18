using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public float hp_max = 100;
    public float hp = 100;
    public float move_speed = 15;
    public bool alive = true;

    abstract public void receive_damage(float damage, string type = "flat");
    abstract public void die();
}
