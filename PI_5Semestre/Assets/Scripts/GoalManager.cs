using System.Collections;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public ResourceManager resources;

    public enum Goals
    {
        First = 200,
        Second = 500,
        Third = 1000,
        Final = 2000
    };
    public Goals currentGoal = Goals.First;

    [HideInInspector] public int strikes;
    public int maxAllowedStrikes;
    float goalCheckInterval;

    void Start()
    {
        int totalGoals = System.Enum.GetValues(typeof(Goals)).Length;
        goalCheckInterval = TimeControls.instance.gameTime / totalGoals;

        StartCoroutine(CheckGoal());
    }

    IEnumerator CheckGoal()
    {
        while (true)
        {
            yield return new WaitForSeconds(goalCheckInterval);

            if (resources.totalGallonsSold >= (int)currentGoal)
            {
                Debug.Log("goal achieved");

                if (currentGoal != Goals.Final)
                {
                    NextGoal();
                }
                else
                {
                    Debug.Log("game ended. well done");

                    yield break;
                }
            }
            else
            {
                strikes++;

                Debug.Log("goal lost. strikes: " + strikes);

                if (currentGoal != Goals.Final)
                {
                    if (strikes >= maxAllowedStrikes)
                    {
                        Debug.Log("game over");

                        yield break;
                    }

                    NextGoal();
                }
                else
                {
                    Debug.Log("game over");

                    yield break;
                }
            }
        }
    }

    public void NextGoal()
    {
        currentGoal = currentGoal switch
        {
            Goals.First => Goals.Second,
            Goals.Second => Goals.Third,
            Goals.Third => Goals.Final,
            _ => Goals.Final
        };

        Debug.Log("next goal: " + currentGoal + " (" + (int)currentGoal + ")");
    }
}