using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class EndOfDayEvent : MonoBehaviour
{
    public bool DayEnded = false;
    public bool isOpen = false;

    public int MaxScore;
    public int WowScore;
    public int YayScore;

    public UnityEvent DayEnd_Event;
    public UnityEvent Meh_Event;
    public UnityEvent Yay_Event;
    public UnityEvent Wow_Event;

    public TextMeshProUGUI DayText;
    public TextMeshProUGUI OrderText;

    void Start()
    {
        if (DayEnd_Event == null)
            DayEnd_Event = new UnityEvent();
    }

    void Update()
    {
        if (DayEnded == true && isOpen == false && DayEnd_Event != null)
        {
            isOpen = true;

            SetText();

            DayEnd_Event.Invoke();

            if (ScoreManager.Score >= WowScore)
            {
                Wow_Event.Invoke();
            }
            if (ScoreManager.Score >= YayScore && ScoreManager.Score < WowScore)
            {
                Yay_Event.Invoke();
            }
            if (ScoreManager.Score < YayScore)
            {
                Meh_Event.Invoke();
            }
        }

        if (DayEnded == false)
        {
            isOpen = false;
        }
    }
    public void SetText()
    {
        // DayText.text = "Day " + DayManager.Day + " completed";

        OrderText.text = ScoreManager.Score.ToString();// + " orders out of " + MaxScore;
    }

}
