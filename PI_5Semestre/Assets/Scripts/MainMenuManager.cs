using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject startPanel;

    public void Play()
    {
        gameObject.SetActive(false);
        startPanel.SetActive(true);
    }

    public void Credits()
    {
        // Exibir painel de créditos
    }

    public void Options()
    {
        // Exibir painel de configurações
    }

    public void Quit()
    {
        Application.Quit();
    }
}
