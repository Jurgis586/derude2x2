using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_launcher : Gun
{
    public float explosion_radius = 3;
    // Start is called before the first frame update
    void Start()
    {
        current_ammo = max_ammo;
        Reload();
    }

    public override void Shoot()
    {
        if (can_shoot)
        {
            if (current_clip > 0)
            {
                var randomNumberX = Random.Range(-accuracy, accuracy);
                var randomNumberY = Random.Range(-accuracy, accuracy);
                var randomNumberZ = Random.Range(-accuracy, accuracy);

                GameObject bullet = Instantiate(projectile, projectile_spawn_point.position
                    , projectile_spawn_point.rotation);

                bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

                bullet.GetComponent<bullet2>().init(damage, projectile_speed, mask);

                current_clip--;
            }
            else
            {
                Reload();
            }
        }
    }

    public override void Reload()
    {
        StartCoroutine(Reload_coroutine());
    }

    IEnumerator Reload_coroutine()
    {
        can_shoot = false;

        player_arsenal PA = GameObject.Find("Player").GetComponentInChildren<player_arsenal>();

        yield return new WaitForSeconds(reload_time);

        current_ammo += current_clip;
        if (current_ammo - max_clip >= 0)
        {
            current_clip = max_clip;
        }
        else
        {
            current_clip = max_clip - current_ammo - max_clip;
        }
        current_ammo -= current_clip;

        can_shoot = true;

        PA.Update_UI();
    }
}
