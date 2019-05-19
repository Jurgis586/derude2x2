using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_gun1 : Gun
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Shoot()
    {
        var randomNumberX = Random.Range(-accuracy, accuracy);
        var randomNumberY = Random.Range(-accuracy, accuracy);
        var randomNumberZ = Random.Range(-accuracy, accuracy);

        GameObject bullet = Instantiate(projectile, projectile_spawn_point.position
            , projectile_spawn_point.rotation);

        bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);

        bullet.GetComponent<bullet2>().init(damage, projectile_speed);
    }

    public override void Reload()
    {
    }
}
