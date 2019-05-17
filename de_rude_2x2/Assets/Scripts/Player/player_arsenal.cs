using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_arsenal : MonoBehaviour
{
    public float fire_rate = 0.5f;
    public int damage = 1;
    public float projectile_speed = 50f;

    public GameObject bullet_prefab;

    private float next_fire_time = 0;
    private Camera cam;

    private Transform player_gun_pos;
    private GameObject gun_obj;
    private Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        gun_obj = GameObject.Find("Gun");
        gun = gun_obj.GetComponent<Gun>();
        player_gun_pos = GameObject.Find("Gun_position").GetComponent<Transform>();
        bullet_prefab = gun.projectile;
        cam = transform.parent.GetComponentInChildren<Camera>();
    }

    void LateUpdate()
    {

        gun_obj.transform.SetPositionAndRotation(player_gun_pos.position, player_gun_pos.rotation);
        if (Input.GetButton("Fire1") && Time.time > next_fire_time)
        {
            next_fire_time = Time.time + fire_rate;
            //Debug.Log("shoot");
            var randomNumberX = Random.Range(-gun.accuracy, gun.accuracy);
            var randomNumberY = Random.Range(-gun.accuracy, gun.accuracy);
            var randomNumberZ = Random.Range(-gun.accuracy, gun.accuracy);

            // Bit shift the index of the layer (9) to get a bit mask
            int layerMask = 1 << 9;
            layerMask = ~layerMask;
            GameObject bullet;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3000f, layerMask))
            {
                //Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.point, Color.green, 2f);
                gun.projectile_spawn_point.transform.LookAt(hit.point);
                bullet = Instantiate(bullet_prefab, gun.projectile_spawn_point.transform.position
                    , gun.projectile_spawn_point.transform.rotation);

                bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            }
            else
            {
                Debug.DrawRay(cam.transform.position, cam.transform.forward * 3000, Color.red, 2f);
                bullet = Instantiate(bullet_prefab, gun.projectile_spawn_point.transform.position
                    , gun.transform.rotation);

                bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            }
            bullet.GetComponent<bullet2>().init(10f);
        }
    }
}
