using UnityEngine;
using TMPro;

public class StatisticsUI : MonoBehaviour
{
    public TMP_Text[] statisticsTxt;
    public TMP_Text scoreTxt, ratingTxt;

    void Start()
    {
        int[] stats = PlayerData.statistics;

        for (int i = 0; i < statisticsTxt.Length; i++)
        {
            if (i < stats.Length)
            {
                statisticsTxt[i].text = stats[i].ToString();
            }
            else
            {
                statisticsTxt[i].text = "N/A";
            }
        }

        scoreTxt.text += PlayerData.score.ToString("F1");
        ratingTxt.text += PlayerData.rating;
    }
}