using UnityEngine;
using System.Collections;
using System;

public class TimeControls : MonoBehaviour
{
    public static TimeControls instance;
    public int gameTime;
    Coroutine timerCoroutine;
    public event Action<int> OnTimeChanged;

    void Awake() => instance = this;

    void Start()
    {
        StartTimer();
    }

    IEnumerator Timer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            DecreaseTime();

            if (gameTime <= 0)
                StopTimer();
        }
    }

    void DecreaseTime()
    {
        gameTime -= 1;
        OnTimeChanged?.Invoke(gameTime);
    }

    public void StartTimer()
    {
        timerCoroutine = StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        if (timerCoroutine != null)
            StopCoroutine(timerCoroutine);
    }

    public void SetSpeed(int value)
    {
        Time.timeScale = value;
    }
}