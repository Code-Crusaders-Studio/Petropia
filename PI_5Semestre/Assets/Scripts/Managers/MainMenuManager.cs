using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject startPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject resultsPanel;
    public TextMeshProUGUI contrastText;
    public TextMeshProUGUI previousScoreText;
    public TextMeshProUGUI previousRatingText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestRatingText;

    void Awake()
    {
        SaveLoadSettings.LoadSettings();
        Debug.Log("firstTime = " + GameSettings.firstTime);
        Debug.Log("audioVolume = " + GameSettings.audioVolume);
        Debug.Log("highContrast = " + GameSettings.highContrast);
    }

    public void Play()
    {
        if (GameSettings.firstTime)
        {
            SaveLoadSettings.SwitchFirstTime();
            menuPanel.SetActive(false);
            startPanel.SetActive(true);
        }
        else
        {
            menuPanel.SetActive(false);
            startPanel.SetActive(true);
        }
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
        menuPanel.SetActive(false);

        if (GameSettings.highContrast)
        {
            contrastText.text = "Ativado";
        }
        else if (!GameSettings.highContrast)
        {

            contrastText.text = "Desativado";
        }
    }

    public void Contrast()
    {
        if (GameSettings.highContrast)
        {
            GameSettings.highContrast = false;
            contrastText.text = "Desativado";
        }
        else if (!GameSettings.highContrast)
        {
            GameSettings.highContrast = true;
            contrastText.text = "Ativado";
        }
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void Results()
    {
        resultsPanel.SetActive(true);
        menuPanel.SetActive(false);

        previousScoreText.text = "Pontuação: " + PlayerData.score.ToString("F1");
        previousRatingText.text = "Nota: " + PlayerData.rating;
        bestScoreText.text = "Pontuação: " + PlayerData.bestScore.ToString("F1");
        bestRatingText.text = "Nota: " + PlayerData.bestRating;
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);

        SaveLoadSettings.SaveSettings();
    }

    public void CloseCredits()
    {

        creditsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void CloseResults()
    {
        resultsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}