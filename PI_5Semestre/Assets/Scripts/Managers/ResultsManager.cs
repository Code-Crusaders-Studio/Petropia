using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsManager : MonoBehaviour
{
    public void GoBack()
    {
        SceneLoader.instance.LoadScene("MainMenu", 1);
    }
}
