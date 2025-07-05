using UnityEngine;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;
    public GameObject menuPanel;
    public TextMeshProUGUI contrastText;

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
            SceneLoader.instance.LoadScene("Gameplay", 1);
        }
        else
        {
            menuPanel.SetActive(false);
            startPanel.SetActive(true);
        }
    }

    public void Credits()
    {
        creditsPanel.SetActive(true);
            menuPanel.SetActive(false);
        
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
        // PlayerPrefs.DeleteAll(); Apaga todo o progresso | Colocar futuramente no painel de opções
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
           menuPanel.SetActive(true);
    }

    public void CloseCredits()
    {

        creditsPanel.SetActive(false);
           menuPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}