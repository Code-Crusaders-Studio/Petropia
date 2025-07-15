using UnityEngine;
using UnityEngine.UI;

public class MenuAudio : MonoBehaviour
{

    public Slider soundSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundSlider.value = GameSettings.audioVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Volume()
    {
        GameSettings.audioVolume = soundSlider.value;


        Debug.Log(GameSettings.audioVolume);
    }
}
