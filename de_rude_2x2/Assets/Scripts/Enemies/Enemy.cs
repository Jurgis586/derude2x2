using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public float hp_max = 100;
    public float hp = 100;
    public float move_speed = 15;
    public bool alive = true;
    public int score = 100;
    protected Rigidbody rb;
    protected float enable_agent_in = 0;

    abstract public void receive_damage(float damage, string type = "flat");
    abstract public void die();
    public void knockback(float force, Vector3 pos, float radius, float upward_mult, float duration = 2)
    {
        enable_agent_in = duration;
        rb.AddExplosionForce(force, pos, radius, upward_mult);
    }

}
