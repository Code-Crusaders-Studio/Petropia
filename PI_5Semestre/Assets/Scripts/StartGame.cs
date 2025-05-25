using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject mainMenu;

    public void NewGame()
    {
        SceneLoader.instance.LoadScene(/*Substituir por cena oficial*/"Progression and Data Test", 1);
    }

    public void Continue()
    {
        // Carregar jogo salvo
    }

    public void Back()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}