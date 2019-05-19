using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class labyrinth_boss : MonoBehaviour
{
    public int stage_1_threshold = 15;
    public int stage_2_threshold = 30;

    public Transform eye_dome;

    public Transform boss_eye;
    public float eye_laser_cooldown;
    public float turn_speed = 2;
    public GameObject enemy_prefab1;
    public Transform enemy_parent;
    private Transform player_pos;
    private LineRenderer line;
    private float next_eye_laser_time = 0;
    private int stage;

    private Transform[] spawn_points;
    private GameObject[] enemies;
    private turret_script[] turrets;

    public float initial_enemy_spawn_cooldown;
    public float enemy_spawn_cooldown;
    public float next_enemy_spawn;
    public int max_enemy_count = 2;
    private int enemy_counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        player_pos = GameObject.Find("Player").GetComponentInChildren<Camera>().transform;
        boss_eye = gameObject.GetComponentInChildren<MeshCollider>().transform.parent;
        line = boss_eye.GetComponentInChildren<LineRenderer>();
        spawn_points = GameObject.Find("lab_enemy_spawns").GetComponentsInChildren<Transform>();
        enemies = new GameObject[max_enemy_count];
        turrets = GetComponentsInChildren<turret_script>();
        next_enemy_spawn = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void activate()
    {
        stage = 1;
    }

    void rand_spawn()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if(enemies[i] == null)
            {
                Transform point = spawn_points[Random.Range(1, spawn_points.Length)];
                enemies[i] = Instantiate(enemy_prefab1, point.position, point.rotation, enemy_parent);
                enemy_counter++;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(stage == 3)
        {
        }
    }

    void LateUpdate()
    {
        if (stage >= 1 && Time.time > next_enemy_spawn)
        {
            if(enemy_counter <= 4)
            {
                next_enemy_spawn = Time.time + initial_enemy_spawn_cooldown;
            }
            else
            {
                next_enemy_spawn = Time.time + enemy_spawn_cooldown;
            }
            rand_spawn();
            if (enemy_counter == stage_1_threshold)
            {
                Debug.Log("turret count: " + turrets.Length);
                for (int i = 0; i < turrets.Length; i++)
                {
                    turrets[i].alive = true;
                }
                stage = 2;
            }
        }
        if (stage == 2)
        {
            if (enemy_counter == stage_2_threshold)
            {
                //remove eye dome
                eye_dome.gameObject.SetActive(false);
                stage = 3;
            }
        }
        if (stage >= 3 && Time.time > next_eye_laser_time)
        {
            Vector3 dir = player_pos.transform.position - boss_eye.position;
            float angle = Vector3.Angle(dir, transform.forward);

            // The step size is equal to speed times frame time.
            float step = turn_speed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(boss_eye.forward, dir, step, 0.0f);
            Debug.DrawRay(boss_eye.position, newDir, Color.red);
            // Move our position a step closer to the target.
            boss_eye.rotation = Quaternion.LookRotation(newDir);


            // shoot lazor
            Ray ray = new Ray(boss_eye.position, boss_eye.forward);
            RaycastHit hit;

            line.enabled = true;
            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, new Vector3(hit.point.x, hit.point.y - 1, hit.point.z));
                if(hit.collider.tag == "Player")
                {
                    hit.transform.GetComponentInChildren<PlayerController>().decreaseLife();
                    next_eye_laser_time = Time.time + eye_laser_cooldown;
                    line.enabled = false;
                }
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(100));
            }

            if (!boss_eye.GetComponentInChildren<Enemy>().alive)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
