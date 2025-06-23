using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    ResourceManager resources;

    [Header("UI Text Display")]
    public TMP_Text cashTxt;
    public TMP_Text oilTxt;
    public TMP_Text gallonTxt;
    public TMP_Text pollutionTxt;

    [Header("Building Panel Components")]
    public GameObject buildingPanel;
    public TMP_Text buildingNameTxt;
    public TMP_Text buildingDescriptionTxt;
    public Button upgradeBtn;
    public Button repairBtn;
    public Button removeBtn;

    [Header("Time Display")]
    public TMP_Text timeTxt;

    void Awake()
    {
        instance = this;
        resources = ResourceManager.instance;
    }

    void OnEnable()
    {
        resources.OnCashChanged += UpdateCashDisplay;
        resources.OnOilChanged += UpdateOilDisplay;
        resources.OnGallonsChanged += UpdateGallonDisplay;
        resources.OnPollutionChanged += UpdatePollutionDisplay;
        resources.OnStorageChanged += UpdateStorageDisplay;
        TimeControls.instance.OnTimeChanged += UpdateTimeDisplay;
    }

    void OnDisable()
    {
        resources.OnCashChanged -= UpdateCashDisplay;
        resources.OnOilChanged -= UpdateOilDisplay;
        resources.OnGallonsChanged -= UpdateGallonDisplay;
        resources.OnPollutionChanged -= UpdatePollutionDisplay;
        resources.OnStorageChanged -= UpdateStorageDisplay;
        TimeControls.instance.OnTimeChanged -= UpdateTimeDisplay;
    }

    void UpdateCashDisplay(int newValue)
    {
        cashTxt.text = "Dinheiro: $" + newValue.ToString("N0");
    }

    void UpdateOilDisplay(int newValue)
    {
        oilTxt.text = "Petróleo: " + newValue + "/" + resources.storageCapacity;
    }

    void UpdateGallonDisplay(int newValue)
    {
        gallonTxt.text = "Galões: " + newValue;
    }

    void UpdatePollutionDisplay(int newValue)
    {
        pollutionTxt.text = "Poluição: " + newValue;
    }

    void UpdateStorageDisplay(int newValue)
    {
        oilTxt.text = "Petróleo: " + resources.oilAmount + "/" + newValue;
    }

    void UpdateTimeDisplay(int timeLeft)
    {
        int totalDuration = 1200;  // Mudar se o game time de TimeControls também mudar (tem que ser o valor inicial de game time)
        int timeElapsed = totalDuration - timeLeft;

        float totalSimulatedYears = 10f;
        float secondsPerSimulatedYear = totalDuration / totalSimulatedYears;
        float monthsPerSecond = 12f / secondsPerSimulatedYear;

        int totalMonths = Mathf.FloorToInt(timeElapsed * monthsPerSecond);

        int year = (totalMonths / 12) + 1;
        int month = (totalMonths % 12) + 1;

        timeTxt.text = $"Ano {year}, Mês {month}";
    }

    public void OpenBuildingPanel(BuildingBase building, GameObject buildingObject, string buildingName, string buildingDescription)
    {
        buildingPanel.SetActive(true);
        buildingNameTxt.text = buildingName;
        buildingDescriptionTxt.text = buildingDescription;

        upgradeBtn.onClick.RemoveAllListeners();
        repairBtn.onClick.RemoveAllListeners();
        removeBtn.onClick.RemoveAllListeners();

        upgradeBtn.onClick.AddListener(() =>
        {
            building.Upgrade();
        });

        repairBtn.onClick.AddListener(() =>
        {
            building.Repair();
        });

        removeBtn.onClick.AddListener(() =>
        {
            Destroy(buildingObject);
            CloseBuildingPanel();
        });

        upgradeBtn.gameObject.SetActive(building.IsUpgradable());
        repairBtn.gameObject.SetActive(building.CanBeRepaired());
    }

    public void CloseBuildingPanel()
    {
        buildingPanel.SetActive(false);
    }

    public void StorageWarning()
    {

    }
}