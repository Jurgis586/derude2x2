using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_arsenal : MonoBehaviour
{
    public GameObject[] guns;

    private float next_fire_time = 0;
    private Camera cam;

    private Quaternion default_gun_rot;
    private Transform player_gun_pos;
    private GameObject curr_gun_obj;
    private Gun curr_gun_script;
    private int gun_index;
    // Start is called before the first frame update
    void Start()
    {
        player_gun_pos = GameObject.Find("Gun_position").GetComponent<Transform>();
        cam = transform.parent.GetComponentInChildren<Camera>();

        guns = GameObject.FindGameObjectsWithTag("Player_gun");
        gun_index = -1;
        if (guns.Length > 0)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                if (gun_index == -1 && guns[i].GetComponent<Gun>().unlocked)
                    gun_index = i;
                guns[i].GetComponent<Gun>().Hide_Gun();
            }
            Select_Gun();
        }
        else
            Debug.Log("ERROR: no guns with tag \"Player_gun\" found");
        default_gun_rot = curr_gun_script.projectile_spawn_point.localRotation;
    }

    void Update()
    {
        float mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if(mousewheel > 0f)
        {
            Next_Gun();
            //Debug.Log("MOUSEWHEEL: " + mousewheel);
        }
        else if (mousewheel < 0f)
        {
            Previous_Gun();
        }
    }

    public void Next_Gun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gun_index++;
            if (gun_index >= guns.Length)
                gun_index = 0;
            if (guns[gun_index].GetComponent<Gun>().unlocked)
            {
                Select_Gun();
                break;
            }
        }
    }

    public void Previous_Gun()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gun_index--;
            if (gun_index < 0)
                gun_index = guns.Length - 1;
            if (guns[gun_index].GetComponent<Gun>().unlocked)
            {
                Select_Gun();
                break;
            }
        }
    }

    private void Select_Gun()
    {

        if(curr_gun_obj != guns[gun_index])
        {
            //hide previous gun
            if (curr_gun_script != null)
                curr_gun_script.Hide_Gun();

            //select new gun and show it
            next_fire_time = Time.time + 1;
            curr_gun_obj = guns[gun_index];
            curr_gun_script = curr_gun_obj.GetComponent<Gun>();
            curr_gun_obj.transform.position = player_gun_pos.position;
            curr_gun_script.Show_Gun();
        }
    }

    void LateUpdate()
    {
        curr_gun_obj.transform.SetPositionAndRotation(player_gun_pos.position, player_gun_pos.rotation);
        if (Input.GetButton("Fire1") && Time.time > next_fire_time)
        {
            next_fire_time = Time.time + curr_gun_script.fire_rate;
            //Debug.Log("shoot");

            // Bit shift the index of the layer (9) to get a bit mask
            int layerMask = 1 << 9;
            layerMask = ~layerMask;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3000f, layerMask))
            {
                //Debug.DrawRay(cam.transform.position, cam.transform.forward * hit.point, Color.green, 2f);
                //curr_gun_script.projectile_spawn_point.transform.LookAt(hit.point);

                // gun.shoot
                curr_gun_script.Shoot();
            }
            else
            {
                //curr_gun_script.projectile_spawn_point.localRotation = default_gun_rot;
                curr_gun_script.Shoot();
            }
        }
    }
}
