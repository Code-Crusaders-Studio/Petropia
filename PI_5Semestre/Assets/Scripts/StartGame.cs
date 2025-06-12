using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject mainMenu;

    public void NewGame()
    {
        SceneLoader.instance.LoadScene("Gameplay", 1);
    }

    public void StartTutorial()
    {
        SceneLoader.instance.LoadScene("Tutorial", 1);
    }

    public void Back()
    {
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }
}