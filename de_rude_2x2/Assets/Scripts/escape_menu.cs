using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escape_menu : MonoBehaviour
{
    private bool escape_menu_enabled;
    private float old_time;
    public GameObject escape_menu_panel;
    public GameObject main_menu;
    public GameObject options_menu;
    public GameObject teleport_menu;
    private GameObject current_menu;

    // Start is called before the first frame update
    void Start()
    {
        escape_menu_enabled = false;
        escape_menu_panel.SetActive(false);
        main_menu.SetActive(false);
        options_menu.SetActive(false);
        teleport_menu.SetActive(false);
        current_menu = main_menu;
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
        escape_menu_enabled = false;
        escape_menu_panel.SetActive(false);
        current_menu.SetActive(false);
        current_menu = main_menu;
        main_menu.SetActive(false);
        Time.timeScale = old_time;
    }


    public void open_menu(GameObject menu)
    {
        current_menu.SetActive(false);
        menu.SetActive(true);
        current_menu = menu;
    }

}
