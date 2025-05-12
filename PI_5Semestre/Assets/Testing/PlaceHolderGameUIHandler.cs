using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlaceHolderGameUIHandler : MonoBehaviour
{
    public static PlaceHolderGameUIHandler instance;

    [Header("UI Text Display")]
    public TMP_Text cashDisplay;
    public TMP_Text oilDisplay;
    public TMP_Text gallonDisplay;
    public TMP_Text pollutionDisplay;

    [Header("Building Panel Components")]
    public GameObject buildingPanel;
    public TMP_Text buildingNameDisplay;
    public Button upgradeBtn;
    public Button repairBtn;
    public Button removeBtn;

    public ResourceManager resourceManager;

    void Awake() => instance = this;

    void Update()
    {
        cashDisplay.text = "Cash: " + resourceManager.Cash;
        oilDisplay.text = "Oil: " + resourceManager.Oil + "/" + resourceManager.OilLimit;
        gallonDisplay.text = "Gallons: " + resourceManager.Gallons;
        pollutionDisplay.text = "Polution: " + resourceManager.Pollution;
    }

    public void OpenBuildingPanel(BuildingBase building, string buildingName)
    {
        buildingPanel.SetActive(true);
        buildingNameDisplay.text = buildingName;

        upgradeBtn.onClick.RemoveAllListeners();
        repairBtn.onClick.RemoveAllListeners();
        removeBtn.onClick.RemoveAllListeners();

        upgradeBtn.onClick.AddListener(() =>
        {
            building.Upgrade();
            CloseBuildingPanel();
        });

        repairBtn.onClick.AddListener(() =>
        {
            building.Repair();
            CloseBuildingPanel();
        });

        removeBtn.onClick.AddListener(() =>
        {
            building.Remove();
            CloseBuildingPanel();
        });

        upgradeBtn.interactable = building.IsUpgradable();
        repairBtn.interactable = building.IsUpgradable();
    }

    public void CloseBuildingPanel()
    {
        buildingPanel.SetActive(false);
    }
}