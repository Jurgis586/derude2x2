using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    public Text pos1;
    public Text pos2;
    public Text pos3;
    public Text pos4;
    public Text pos5;
    public Text pos6;
    public Text pos7;
    public Text pos8;
    public Text pos9;
    public Text pos10;

    public void Start()
    {
        setScores();
    }

    // Resets scores string
    public void Reset()
    {
        PlayerPrefs.SetString("Scores", "");
        setScores();
    }

    // Sets scores
    private void setScores()
    {
        Text[] posArray = new Text[10] { pos1, pos2, pos3, pos4, pos5, pos6, pos7, pos8, pos9, pos10 };
        string[] scores = PlayerPrefs.GetString("Scores").Split(',');

        for (int i = 0; i < scores.Length && i < 10; i++)
            posArray[i].text = scores[i];
    }
}
