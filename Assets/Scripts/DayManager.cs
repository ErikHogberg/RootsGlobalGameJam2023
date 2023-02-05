using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static int Day;

    public void IncreaseDay()
    {
        Day = Day + 1;
    }
    public void DecreaseDay()
    {
        Day = Day - 1;
    }
    public void ResetDay()
    {
        Day = 0;
    }

    public void SetDay(int number)
    {
        Day = number;
    }
}
