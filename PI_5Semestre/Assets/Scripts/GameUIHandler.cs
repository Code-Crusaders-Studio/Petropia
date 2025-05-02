using UnityEngine;
using TMPro;

public class GameUIHandler : MonoBehaviour
{
    public TMP_Text cashDisplay, oilDisplay, gallonDisplay, polutionDisplay;
    public ResourceManager resourceManager;

    void Update()
    {
        cashDisplay.text = "Cash: " + resourceManager.Cash;
        oilDisplay.text = "Oil: " + resourceManager.Oil + "/" + resourceManager.OilLimit;
        gallonDisplay.text = "Gallons: " + resourceManager.Gallons;
        polutionDisplay.text = "Polution: " + resourceManager.Pollution;
    }
}
