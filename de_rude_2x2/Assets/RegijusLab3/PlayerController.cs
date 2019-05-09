using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
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
        lives = 3;
        score = 0;
        setScoreText();
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
