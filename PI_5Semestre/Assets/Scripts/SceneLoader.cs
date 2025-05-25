using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    Animator anim;

    void Awake() => instance = this;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public IEnumerator Transition(string sceneName, float waitTime)
    {
        // Tocar animação de transição
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName, float waitTime)
    {
        StartCoroutine(Transition(sceneName, waitTime));
    }
}