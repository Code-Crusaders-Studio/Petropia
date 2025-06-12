using UnityEngine;

public class LoadData : MonoBehaviour
{
    void Start()
    {
        PlayerData.score = PlayerPrefs.GetFloat("LastPlayerScore", 0f);
        PlayerData.rating = PlayerPrefs.GetString("LastPlayerRating", "");
        PlayerData.bestScore = PlayerPrefs.GetFloat("BestPlayerScore", 0f);
        PlayerData.bestRating = PlayerPrefs.GetString("BestPlayerRating", "");

        Debug.Log("data loaded successfully");
    }
}