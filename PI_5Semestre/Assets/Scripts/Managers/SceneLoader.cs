using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    Animator anim;
    AudioSource aud;
    string currentScene;

    public AudioClip[] song;

    AudioClip newClip;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);



    }

    void Start()
    {
        anim = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += ChangedActiveScene;

        aud.clip = song[0];
        aud.Play();

    }

    public IEnumerator Transition(string sceneName, float waitTime)
    {
        // Tocar animação de transição
        yield return new WaitForSecondsRealtime(waitTime);
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName, float waitTime)
    {
        StartCoroutine(Transition(sceneName, waitTime));
    }
<<<<<<< Updated upstream
}
=======
<<<<<<< Updated upstream
}
=======

    void ChangedActiveScene(Scene current, Scene next)
    {
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Gameplay")
      {
            newClip = song[1];

            if (newClip != aud.clip)
            {
                aud.clip = newClip;
                aud.Play();
            
            }
        
      }
      else if (currentScene != "Gameplay")
      {
            newClip = song[0];
            if (newClip != aud.clip)
            {
                aud.clip = newClip;
                aud.Play();

            }
      }

    }
}
>>>>>>> Stashed changes
>>>>>>> Stashed changes
