using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startPanel;

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
        // Exibir painel de configurações
        // PlayerPrefs.DeleteAll(); Apaga todo o progresso | Colocar futuramente no painel de opções
    }

    public void Quit()
    {
        Application.Quit();
    }
}