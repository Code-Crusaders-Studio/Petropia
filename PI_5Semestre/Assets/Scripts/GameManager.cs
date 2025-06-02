using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnGameFinished(int strikes, int cashEarned, int oilExtracted, int gallonsProduced, int gallonsSold, int pollutionCaused)
    {
        PlayerData.rating = Rating(strikes, cashEarned, oilExtracted, gallonsProduced, gallonsSold, pollutionCaused);

        SceneLoader.instance.LoadScene("Results", 1);
    }

    public void OnGameOver()
    {
        SceneLoader.instance.LoadScene("GameOver", 1);
    }

    public string Rating(int strikes, int cashEarned, int oilExtracted, int gallonsProduced, int gallonsSold, int pollutionCaused)
    {
        PlayerData.statistics = new int[] { strikes, cashEarned, oilExtracted, gallonsProduced, gallonsSold, pollutionCaused };

        PlayerData.score += Mathf.Clamp01(1f - (strikes / 3f)) * 20f;
        PlayerData.score += Mathf.Clamp01(cashEarned / 100000000f) * 15f;
        PlayerData.score += Mathf.Clamp01(oilExtracted / 100000f) * 15f;
        PlayerData.score += Mathf.Clamp01(gallonsProduced / 100000f) * 15f;
        PlayerData.score += Mathf.Clamp01(gallonsSold / 10000f) * 25f;
        PlayerData.score += Mathf.Clamp01(1f - (pollutionCaused / 500000f)) * 10f; 

        if (PlayerData.score >= 90f)
            return "S";
        else if (PlayerData.score >= 75f)
            return "A";
        else if (PlayerData.score >= 60f)
            return "B";
        else if (PlayerData.score >= 45f)
            return "C";
        else if (PlayerData.score >= 30f)
            return "D";
        else
            return "E";
    }
}