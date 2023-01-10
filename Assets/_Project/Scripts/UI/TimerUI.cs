using GameCore;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TimerUI : MonoBehaviour
{
    #region Private Fields

    private TMP_Text _text;

    #endregion

    #region Unity LifeCycle

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        SetText(GetTimer());
    }

    private void Update()
    {
        SetText(GetTimer());
    }

    #endregion

    #region Helper Methods

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

    #endregion
}