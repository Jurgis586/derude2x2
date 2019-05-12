using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_arsenal : MonoBehaviour
{
    private Transform player_gun_pos;
    private GameObject gun;
    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("Gun");
        player_gun_pos = GameObject.Find("Gun_position").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        gun.transform.SetPositionAndRotation(player_gun_pos.position, player_gun_pos.rotation);
    }
}
