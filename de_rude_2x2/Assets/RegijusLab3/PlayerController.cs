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
    public Transform player;
    // Score
    public Text scoreText;
    public int score;

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
        // Set default GUI components
        Time.timeScale = 1;
        lives = 3;
        score = 0;
        setScoreText();

        // Set spawn location
        Vector3 spawn1 = new Vector3(270, 3, 250); // City gate
        Vector3 spawn2 = new Vector3(-120, 3, 480); // Pyramid
        Vector3 spawn3 = new Vector3(-410, 3, 760); // Labyrinth 
        Vector3 spawn4 = new Vector3(160, 3, 720); // Temple
        Vector3 spawn5 = new Vector3(0, 3, 200); // Sea

        switch (PlayerPrefs.GetString("Spawn")) {
            case "City_Entrance":
                player.position = spawn1;
                break;
            case "Pyramid":
                player.position = spawn2;
                break;
            case "Labyrinth":
                player.position = spawn3;
                break;
            case "Temple":
                player.position = spawn4;
                break;
            case "Sea":
                player.position = spawn5;
                break;
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
    
    public void backToMainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
