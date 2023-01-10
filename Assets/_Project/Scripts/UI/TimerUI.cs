using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TimerUI : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        SetText(GetTimer());
    }

    private void Update()
    {
        SetText(GetTimer());
    }

    private float GetTimer()
    {
        return GameManager.Instance.GetTimer();
    }

    private void SetText(float timer)
    {
        _text.text = ParseTimer(timer);
    }

    private string ParseTimer(float timer)
    {
        return ((int) (timer / 60)).ToString("D2") + ":" + ((int) (timer % 60)).ToString("D2");
    }
}