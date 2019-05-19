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
    public MovementRB player_mov;
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

    // Lives
    public int lives;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    // Game Over
    public Text gameOverText;
    public Text finalScore;
    public GameObject mainMenuButton;

    void Start() {
        if (lock_cursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        player_mov = player.GetComponentInChildren<MovementRB>();
        Time.timeScale = 1;
        lives = 3;
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

    // Increase life by one
    public void increaseLife() {
        lives++;
        checkIsPlayerAlive();
    }

    // Decreases life by one
    public void decreaseLife() {
        lives--;
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
        switch(lives) {
            case 3:
                heart3.enabled = true;
                heart2.enabled = true;
                heart1.enabled = true;
                break;
            case 2:
                heart3.enabled = false;
                heart2.enabled = true;
                heart1.enabled = true;
                break;
            case 1:
                heart3.enabled = false;
                heart2.enabled = false;
                heart1.enabled = true;
                break;
            case 0:
                heart3.enabled = false;
                heart2.enabled = false;
                heart1.enabled = false;

                // Game over condition met
                scoreText.enabled = false;
                gameOverText.text = "GAME OVER";
                finalScore.text = "Score: " + score.ToString();
                mainMenuButton.SetActive(true);
                saveScore(); // saving high score
                Time.timeScale = 0; // this will freeze the game
                break;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            decreaseLife();
            changeScore(-50);
        }

        if (collision.gameObject.CompareTag("Score")) {
            changeScore(100);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Health") && lives != 3) {
            increaseLife();
            changeScore(100);
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
                    increaseLife();
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
