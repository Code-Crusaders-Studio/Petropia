using UnityEngine;
using System.Collections;

public class TimeControls : MonoBehaviour
{
    public static TimeControls instance;
    public int gameTime;
    Coroutine timerCoroutine;

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

            gameTime -= 1;

            if (gameTime <= 0)
                StopTimer();
        }
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