using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timed_object_spawner : MonoBehaviour
{
    public bool active = true;
    public bool wait_for_despawn = true;
    public float spawn_period = 5f;
    private float next_spawn = 0f;
    public GameObject prefab;
    private GameObject spawned_obj;
    // Start is called before the first frame update
    void Start()
    {
        next_spawn = Time.time + spawn_period;
    }

    // Update is called once per frame
    void Update()
    {
        if (active && next_spawn < Time.time/* && spawned_obj == null*/)
        {
            if(!wait_for_despawn || spawned_obj == null)
            {
                next_spawn = Time.time + spawn_period;
                spawned_obj = Instantiate(prefab, transform.position, transform.rotation);
            }
        }
    }
    public void Spawn()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
    public void Spawn(GameObject obj)
    {
        Instantiate(obj, transform.position, transform.rotation);
    }
}
