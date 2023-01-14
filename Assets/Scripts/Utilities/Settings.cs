using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    private const float fadeDuration = 0.35f;
    private const float targetAlpha = 0.45f;
    private const float tooltipOffset = 50;

    public static float FadeDuration => fadeDuration;
    public static float TargetAlpha => targetAlpha;
    public static float TooltipOffset => tooltipOffset;
    public static float SecondThreshold => 0.1f;
    public static int SecondsPerMinute => 60;
    public static int MinutesPerHour => 60;
    public static int HoursPerDay => 24;
    public static int DaysPerMonth => 30;
    public static int MonthsPerYear => 12;
    public static int SeasonPerYear => Enum.GetValues(typeof(Season)).Length;
    // one year will go over all seasons once
    public static int MonthsPerSeason => MonthsPerYear / SeasonPerYear;
}
