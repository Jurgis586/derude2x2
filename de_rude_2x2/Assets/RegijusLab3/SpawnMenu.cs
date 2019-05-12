using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMenu : MonoBehaviour
{
    private void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SpawnCityEntrance() {
        PlayerPrefs.SetString("Spawn", "City_Entrance");
        PlayGame();
    }

    public void SpawnSea() {
        PlayerPrefs.SetString("Spawn", "Sea");
        PlayGame();
    }

    public void SpawnPyramid() {
        PlayerPrefs.SetString("Spawn", "Pyramid");
        PlayGame();
    }

    public void SpawnTemple() {
        PlayerPrefs.SetString("Spawn", "Temple");
        PlayGame();
    }

    public void SpawnLabyrinth() {
        PlayerPrefs.SetString("Spawn", "Labyrinth");
        PlayGame();
    }
}
