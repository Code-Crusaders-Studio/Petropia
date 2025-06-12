using UnityEngine;

public class SaveLoadSettings : MonoBehaviour
{
    public static void SwitchFirstTime()
    {
        PlayerPrefs.SetInt("FirstTime", 0);
        PlayerPrefs.Save();
    }

    public static void SaveSettings() // Chamar quando fechar o menu de Opções, seja no menu ou no jogo
    {
        PlayerPrefs.SetFloat("AudioVolume", GameSettings.audioVolume);
        PlayerPrefs.SetInt("HighContrast", GameSettings.highContrast ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("settings saved successfully");
    }

    public static void LoadSettings() // Chamar quando iniciar o menu principal
    {
        GameSettings.firstTime = PlayerPrefs.GetInt("FirstTime", 1) == 1;
        GameSettings.audioVolume = PlayerPrefs.GetFloat("AudioVolume", 1.0f);
        GameSettings.highContrast = PlayerPrefs.GetInt("HighContrast", 0) == 1;

        Debug.Log("settings loaded successfully");
    }
}