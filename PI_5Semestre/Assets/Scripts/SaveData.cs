using UnityEngine;

public class SaveData : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetFloat("LastPlayerScore", PlayerData.score);
        PlayerPrefs.SetString("LastPlayerRating", PlayerData.rating);

        if (PlayerData.score > PlayerData.bestScore)
        {
            PlayerPrefs.SetFloat("BestPlayerScore", PlayerData.score);
            PlayerPrefs.SetString("BestPlayerRating", PlayerData.rating);
        }

        PlayerPrefs.Save();

        Debug.Log("data saved successfully");
    }
}