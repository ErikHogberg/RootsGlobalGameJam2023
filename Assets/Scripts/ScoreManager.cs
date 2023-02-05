using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static int Score = 0;
    public TextMeshProUGUI ScoreText;
    int scoreCache = -1;

    private void Update() {
        if(scoreCache != Score){
            scoreCache = Score;
            ScoreText.SetText(Score.ToString());
        }
    }

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
