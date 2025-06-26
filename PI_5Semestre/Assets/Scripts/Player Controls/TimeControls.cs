using UnityEngine;
using System.Collections;
using System;

public class TimeControls : MonoBehaviour
{
    public static TimeControls instance;
    public int totalGameTime;
    [HideInInspector] public int gameTime = 0;
    public event Action<int> OnTimeChanged;

    void Awake() => instance = this;

    void Start()
    {
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (gameTime < totalGameTime)
        {
            yield return new WaitForSeconds(1);

            PassTime();
        }
    }

    void PassTime()
    {
        gameTime++;
        OnTimeChanged?.Invoke(gameTime);
        //Debug.Log("gameTime: " + gameTime);
    }

    public void SetSpeed(int value)
    {
        Time.timeScale = value;
    }
}