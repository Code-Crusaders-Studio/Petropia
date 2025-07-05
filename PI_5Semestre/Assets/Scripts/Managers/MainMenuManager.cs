using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

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
            SceneLoader.instance.LoadScene("Tutorial", 1);
        }
        else
        {
            gameObject.SetActive(false);
            startPanel.SetActive(true);
        }
    }

    public void Credits()
    {
        // Exibir painel de créditos
    }

    public void Options()
    {
        optionsPanel.SetActive(false);
        // PlayerPrefs.DeleteAll(); Apaga todo o progresso | Colocar futuramente no painel de opções
    }

    public void CloseOptions()
    {

    }

    public void CloseCredits()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }
}