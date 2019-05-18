using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_arsenal : MonoBehaviour
{
    public float fire_rate = 0.5f;
    public int damage = 1;
    public float projectile_speed = 50f;

    public GameObject bullet_prefab;
    public GameObject[] guns;

    private float next_fire_time = 0;
    private Camera cam;

    private Transform player_gun_pos;
    private GameObject gun_obj;
    private Gun selected_gun;
    // Start is called before the first frame update
    void Start()
    {
        guns = GameObject.FindGameObjectsWithTag("Player_gun");
        if(guns.Length > 0)
        {

        }
        gun_obj = GameObject.Find("Gun");
        selected_gun = gun_obj.GetComponent<Gun>();
        player_gun_pos = GameObject.Find("Gun_position").GetComponent<Transform>();
        bullet_prefab = selected_gun.projectile;
        cam = transform.parent.GetComponentInChildren<Camera>();
    }



    void LateUpdate()
    {

        gun_obj.transform.SetPositionAndRotation(player_gun_pos.position, player_gun_pos.rotation);
        if (Input.GetButton("Fire1") && Time.time > next_fire_time)
        {
            next_fire_time = Time.time + fire_rate;
            //Debug.Log("shoot");
            var randomNumberX = Random.Range(-selected_gun.accuracy, selected_gun.accuracy);
            var randomNumberY = Random.Range(-selected_gun.accuracy, selected_gun.accuracy);
            var randomNumberZ = Random.Range(-selected_gun.accuracy, selected_gun.accuracy);

            // Bit shift the index of the layer (9) to get a bit mask
            int layerMask = 1 << 9;
            layerMask = ~layerMask;
            GameObject bullet;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3000f, layerMask))
            {
                //Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.point, Color.green, 2f);
                selected_gun.projectile_spawn_point.transform.LookAt(hit.point);
                bullet = Instantiate(bullet_prefab, selected_gun.projectile_spawn_point.transform.position
                    , selected_gun.projectile_spawn_point.transform.rotation);

                bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            }
            else
            {
                Debug.DrawRay(cam.transform.position, cam.transform.forward * 3000, Color.red, 2f);
                bullet = Instantiate(bullet_prefab, selected_gun.projectile_spawn_point.transform.position
                    , selected_gun.transform.rotation);

                bullet.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
            }
            bullet.GetComponent<bullet2>().init(10f);
        }
    }
}
