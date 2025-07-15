using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    public string[] dialog;

    int tutorialID = 0;

    public TextMeshProUGUI tutorialText;

    public Sprite[] pictures;

    public Image tutorialDisplay;


    void Start()
    {
        
    }

    void Update()
    {
       
    }

    public void Next()
    {   
        if (tutorialID == 24)
        {
            SceneLoader.instance.LoadScene("Gameplay", 0);
        }

        tutorialID++;
        tutorialDisplay.sprite = pictures[tutorialID];
        tutorialText.text = dialog[tutorialID];
    }
}
