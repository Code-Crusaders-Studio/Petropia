using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startPanel;
    public static bool firstTime = true;

    void Awake()
    {
        firstTime = PlayerPrefs.GetInt("FirstTime", 1) == 1;
    }

    public void Play()
    {
        if (firstTime)
        {
            firstTime = false;
            PlayerPrefs.SetInt("FirstTime", 0);
            PlayerPrefs.Save();
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