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
        lives = 3;
        score = 0;
        setScoreText();

        // Set spawn location
        int spawnCount = 5;
        Vector3 spawn1 = new Vector3(270, 3, 250); // City gate
        Vector3 spawn2 = new Vector3(-120, 3, 480); // Pyramid
        Vector3 spawn3 = new Vector3(-410, 3, 760); // Labyrinth 
        Vector3 spawn4 = new Vector3(160, 3, 720); // Temple
        Vector3 spawn5 = new Vector3(0, 3, 200); // Sea
        Vector3[] spawns = new Vector3[spawnCount];
        spawns[0] = spawn1;
        spawns[1] = spawn2;
        spawns[2] = spawn3;
        spawns[3] = spawn4;
        spawns[4] = spawn5;

        // Randomize spawn
        int randomizedValue = Random.Range(0, 5);
        player.position = spawns[randomizedValue];
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

                scoreText.enabled = false;
                gameOverText.text = "GAME OVER";
                finalScore.text = "Score: " + score.ToString();
                mainMenuButton.SetActive(true);
                Time.timeScale = 0; // this will freeze the game
                break;
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            decreaseLife();
            changeScore(-50);
        }

        if (collision.gameObject.CompareTag("Health") && lives != 3) {
            increaseLife();
            changeScore(100);
        }
    }

    public void backToMainMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
