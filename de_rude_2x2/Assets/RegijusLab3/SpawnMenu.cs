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
        PlayerPrefs.SetString("StartSpawn", "City_Entrance");
        PlayGame();
    }

    public void SpawnSea() {
        PlayerPrefs.SetString("StartSpawn", "Sea");
        PlayGame();
    }

    public void SpawnPyramid() {
        PlayerPrefs.SetString("StartSpawn", "Pyramid");
        PlayGame();
    }

    public void SpawnTemple() {
        PlayerPrefs.SetString("StartSpawn", "Temple");
        PlayGame();
    }

    public void SpawnLabyrinth() {
        PlayerPrefs.SetString("StartSpawn", "Labyrinth");
        PlayGame();
    }
}
