using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int Score;
    public TextMeshProUGUI ScoreText;


    public void IncreaseScore()
    {
        Score = Score + 1;
    }
    public void DecreaseScore()
    {
        Score = Score - 1;
    }
    public void ResetScore()
    {
        Score = 0;
    }
}
