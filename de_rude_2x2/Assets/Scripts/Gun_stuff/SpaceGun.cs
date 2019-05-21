using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGun : Gun
{
    public AudioSource reloadSound;
    public AudioSource shootSound;

    public override void Shoot()
    {
        if (can_shoot)
        {
            if (current_clip > 0)
            {
                shootSound.Play();
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
        reloadSound.Play();
        StartCoroutine(Reload_coroutine());
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
    }

    IEnumerator Reload_coroutine()
    {
        can_shoot = false;
        yield return new WaitForSeconds(reload_time);
        can_shoot = true;
    }
}
