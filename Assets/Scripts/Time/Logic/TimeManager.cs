using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TimeManager : MonoBehaviour
{
    private int GameSecond { get; set; } = 1;
    private int GameMinute { get; set; } = 1;
    private int GameHour { get; set; } = 1;
    private int GameDay { get; set; } = 1;
    private int GameMonth { get; set; } = 1;
    private int GameYear { get; set; } = 1;
    private Season GameSeason { get; set; } = 0;
    private int MonthInSeason { get; set; } = 1;
    public bool IsGamePaused { get; set;} = false;
    private float tikTime { get; set; } = 0;

    private void Awake()
    {
        NewGameTime();
    }

    private void Update()
    {
        if (!IsGamePaused)
        {
            tikTime += Time.deltaTime;

            if (tikTime>= Settings.SecondThreshold)
            {
                tikTime -= Settings.SecondThreshold;
                UpdateGameTime();
            }
        }
    }

    private void NewGameTime()
    {
        GameSecond = 0;
        GameMinute = 0;
        GameHour = 6;
        GameDay = 1;
        GameMonth = 1;
        GameYear = 1;
        GameSeason = 0;
    }
    private void UpdateGameTime()
    {
        GameSecond++;
        if (GameSecond >= Settings.SecondsPerMinute)
        {
            GameMinute++;
            GameSecond = 0;

            if (GameMinute >= Settings.MinutesPerHour)
            {
                GameHour++;
                GameHour = 0;

                if (GameHour > Settings.HoursPerDay)
                {
                    GameDay++;
                    GameHour = 1;

                    if(GameDay > Settings.DaysPerMonth)
                    {
                        GameMonth++;
                        GameDay = 1;

                        if (GameMonth > Settings.MonthsPerYear)
                        {
                            GameYear++;
                            GameMonth = 1;

                            if (GameYear > 9999)
                            {

                            }
                        }

                        MonthInSeason++;
                        if (MonthInSeason > Settings.MonthsPerSeason)
                        {
                            GameSeason++;

                            if (!Enum.IsDefined(typeof(Season), GameSeason))
                                GameSeason = 0;
                        }
                    }
                }
            }
        }
        Debug.Log("Second: " + GameSecond + " Minute: " + GameMinute);
    }
}
