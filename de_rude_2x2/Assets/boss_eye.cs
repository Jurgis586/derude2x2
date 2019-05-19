using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_eye : Enemy
{
    void Start()
    {
        hp = hp_max;
    }

    public override void receive_damage(float damage, string type = "flat")
    {
        switch (type)
        {
            // percent from current hp
            case "per_cur":
                hp = hp / 100 * damage;
                break;

            // percent from max hp
            case "per_max":
                hp -= hp_max / 100 * damage;
                break;

            // flat damage to hp
            default:
                hp -= damage;
                break;
        }
        if (alive && hp <= 0)
            die();
    }

    public override void die()
    {
        alive = false;
    }
}
