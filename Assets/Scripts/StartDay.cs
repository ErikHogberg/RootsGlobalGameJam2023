using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDay : MonoBehaviour
{
    void Start()
    {
        DayManager.Day++;
        ScoreManager.Score = 0;
    }
}
