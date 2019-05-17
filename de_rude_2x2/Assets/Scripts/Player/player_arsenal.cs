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
        cam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        gun_obj.transform.SetPositionAndRotation(player_gun_pos.position, player_gun_pos.rotation);
        if (Input.GetButton("Fire1") && Time.time > next_fire_time)
        {
            next_fire_time = Time.time + fire_rate;
            //Debug.Log("shoot");
            GameObject obj = Instantiate(bullet_prefab, gun.projectile_spawn_point.transform.position
                , cam.transform.rotation);
            obj.GetComponent<bullet2>().init(10f);
        }
    }
}
