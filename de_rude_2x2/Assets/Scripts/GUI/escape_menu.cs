using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class escape_menu : MonoBehaviour
{
    private bool escape_menu_enabled;
    private float old_time;
    public GameObject escape_menu_panel;
    public GameObject main_menu;
    public GameObject options_menu;
    public GameObject teleport_menu;
    public Dropdown teleport_dropdown;
    private GameObject current_menu;
    private Transform[] spawn_positions;

    // Start is called before the first frame update
    void Start()
    {
        escape_menu_enabled = false;
        escape_menu_panel.SetActive(false);
        main_menu.SetActive(false);
        options_menu.SetActive(false);
        teleport_menu.SetActive(false);
        current_menu = main_menu;
        spawn_positions = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        teleport_dropdown = teleport_menu.GetComponentInChildren<Dropdown>();
        set_spawns_dropdown();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escape_menu_enabled)
                close_escape_menu();
            else
                open_escape_menu();
        }
    }

    void open_escape_menu()
    {
        old_time = Time.timeScale;
        Time.timeScale = 0;

        escape_menu_panel.SetActive(true);
        open_menu(main_menu);

        // open canvases
        escape_menu_enabled = true;
    }

    public void close_escape_menu()
    {
        // close canvases
        current_menu.SetActive(false);
        current_menu = main_menu;
        main_menu.SetActive(false);
        escape_menu_enabled = false;
        escape_menu_panel.SetActive(false);
        Time.timeScale = old_time;
    }


    public void open_menu(GameObject menu)
    {
        current_menu.SetActive(false);
        menu.SetActive(true);
        current_menu = menu;
    }

    void set_spawns_dropdown()
    {
        List<string> names = new List<string>();

        foreach (var item in spawn_positions)
            names.Add(item.name);

        teleport_dropdown.ClearOptions();
        teleport_dropdown.AddOptions(names);

    }

    public void teleport_to(string location_name)
    {
        for (int i = 0; i < spawn_positions.Length; i++)
        {
            if(location_name == spawn_positions[i].name)
            {
                GameObject.Find("Player").transform.SetPositionAndRotation(spawn_positions[i].position
                    , spawn_positions[i].rotation);
                break;
            }
        }
    }
    public void teleport_to(int index)
    {
        Debug.Log(spawn_positions[index].position);
        
        close_escape_menu();

        GameObject.Find("Player").transform.SetPositionAndRotation(spawn_positions[index].position
            , spawn_positions[index].rotation);
    }

}
