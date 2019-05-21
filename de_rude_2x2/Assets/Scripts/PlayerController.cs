using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Player
    public GameObject player;
    private MovementRB player_mov;
    private float speed_duration = 0;
    public bool teleport_on_start = true;
    public bool lock_cursor = false;

    // Score
    public Text scoreText;
    public int score;

    // Teleports
    public GameObject City_Entrance;
    public GameObject Pyramid;
    public GameObject Labyrinth;
    public GameObject Temple;
    public GameObject Sea;

    // Health
    public float max_health;
    public float health;
    public Image healthBar;

    // Game Over
    public Text gameOverText;
    public Text finalScore;
    public GameObject mainMenuButton;
    public GameObject crosshair;

    void Start() {
        if (lock_cursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        player_mov = player.GetComponentInChildren<MovementRB>();
        Time.timeScale = 1;
        health = max_health;
        score = 0;
        setScoreText();

        if (teleport_on_start)
        {
            switch (PlayerPrefs.GetString("StartSpawn"))
            {
                case "City_Entrance":
                    player.transform.position = City_Entrance.transform.position;
                    break;
                case "Pyramid":
                    player.transform.position = Pyramid.transform.position;
                    break;
                case "Labyrinth":
                    player.transform.position = Labyrinth.transform.position;
                    break;
                case "Temple":
                    player.transform.position = Temple.transform.position;
                    break;
                case "Sea":
                    player.transform.position = Sea.transform.position;
                    break;
            }
        }
    }

    // Sets score text for GUI
    void setScoreText() {
        scoreText.text = "Score: " + score.ToString();
    }

    // Changes score by value specified
    public void changeScore(int value) {
        score += value;
        setScoreText();
    }

    // Changes health by amount specified
    public void changeHealthBy(float amount)
    {
        if (health + amount >= max_health)
        {
            health = max_health;
            healthBar.fillAmount = 1f;
        }
        else
        {
            health += amount;
            healthBar.fillAmount = (health / max_health);
        }
        checkIsPlayerAlive();
    }

    // Saves players' score
    private void saveScore() {
        List<string> scores = PlayerPrefs.GetString("Scores").Split(',').ToList();

        int i;
        for (i = 0; i < scores.Count; i++)
            if (scores[i] == "" || score > Int32.Parse(scores[i]))
                break;

        if (i != scores.Count) {
            scores.Insert(i, score.ToString());
        } else {
            scores.Insert(scores.Count, score.ToString());
        }

        PlayerPrefs.SetString("Scores", string.Join(",", scores));
    }

    // Checks if player is still alive
    void checkIsPlayerAlive() {

        if (health <= 0)
        {
            player_mov.player_is_active = false;
            scoreText.enabled = false;
            gameOverText.text = "GAME OVER";
            finalScore.text = "Score: " + score.ToString();
            mainMenuButton.SetActive(true);
            saveScore(); // saving high score
            crosshair.SetActive(false);
            Time.timeScale = 0; // this will freeze the game
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            changeHealthBy((max_health / 10) * -1);
        }

        if (collision.gameObject.CompareTag("Score")) {
            changeScore(100);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Health") && health != max_health) {
            changeHealthBy(max_health);
            Destroy(collision.gameObject);
        }
    }

    public void apply_effect(string effect_name, float value)
    {
        switch (effect_name)
        {
            case "speed":
                Debug.Log("speed");
                player_mov.change_speed(value, 10);
                break;

            case "health":
                for (int i = 0; i < value; i++)
                    changeHealthBy(max_health);
                break;

            case "ammo":
                break;

            default:
                break;
        }
    }


    public void backToMainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
