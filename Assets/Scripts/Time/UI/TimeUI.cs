using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    [field: SerializeField] public RectTransform DayNightImage { get; set; }
    [field: SerializeField] public RectTransform ClockParent { get; set; }
    [field: SerializeField] public Image SeasonImage { get; set; }
    [field: SerializeField] public TextMeshProUGUI DateText { get; set; }
    [field: SerializeField] public TextMeshProUGUI TimeText { get; set; }
    [field: SerializeField] public Sprite[] SeasonSprites { get; set; }

    private List<GameObject> ClockBlocks = new();

    private void Awake()
    {
        for (int i = 0; i < ClockParent.childCount; i++)
        {
            GameObject clockBlock = ClockParent.GetChild(i).gameObject;
            ClockBlocks.Add(clockBlock);
            clockBlock.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EventHandler.GameMinuteEvent += OnGameMinuteEvent;
        EventHandler.GameDateEvent += OnGameDateEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameMinuteEvent -= OnGameMinuteEvent;
        EventHandler.GameDateEvent -= OnGameDateEvent;
    }

    private void OnGameMinuteEvent(int minute, int hour)
    {
        TimeText.text = hour.ToString("00") + ":" + minute.ToString("00");
    }
    private void OnGameDateEvent(int hour, int day, int month, int year, Season season)
    {
        DateText.text = year.ToString("0000") + "-" + month.ToString("00") + "-" + day.ToString("00");
        SeasonImage.sprite = SeasonSprites[(int)season];

        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }

    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;

        /*if (index == 0)
        {
            foreach (var clockBlock in ClockBlocks)
            {
                clockBlock.SetActive(false);
            }
        }
        else
        {*/
            for (int i = 0; i < ClockBlocks.Count; i++)
            {
                if (i <= index)
                    ClockBlocks[i].SetActive(true);
                else
                    ClockBlocks[i].SetActive(false);
            }
        //}
    }

    private void DayNightImageRotate(int hour)
    {
        Vector3 target = new(0, 0, hour * 15 - 90);
        DayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }
}
