using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    ResourceManager resources;
    GoalManager goals;

    [Header("UI Text Display")]
    public TMP_Text cashTxt;
    public TMP_Text oilTxt;
    public TMP_Text gallonTxt;
    public TMP_Text pollutionTxt;

    [Header("Building Panel Components")]
    public GameObject buildingPanel;
    public GameObject buildingPanelShop;
    public TMP_Text buildingNameTxt;
    public TMP_Text buildingDescriptionTxt;
    public Button upgradeBtn;
    public Button repairBtn;
    public Button removeBtn;

    [Header("Time Display")]
    public TMP_Text timeTxt;

    //

    public Image goalBar;
    public GameObject[] goal;
    bool[] fails = { false, false, false };

    public GameObject[] check;

    public Sprite[] goalResult;

    void Start()
    {
        for (int i = 0; i < check.Length; i++)
        {
           check[i].SetActive(false);     
        }
       
    }

    void Update()
    {
        goalBar.fillAmount = resources.totalGallonsSold / 2000f;

        if (goals.currentGoal == GoalManager.Goals.First)
        {
            goal[0].SetActive(true);
            goal[1].SetActive(false);
            goal[2].SetActive(false);
            goal[3].SetActive(false);
        }

        if (goals.currentGoal == GoalManager.Goals.Second)
        {

            goal[0].SetActive(false);
            goal[1].SetActive(true);
            goal[2].SetActive(false);
            goal[3].SetActive(false);

            if (resources.totalGallonsSold < 200)
            {
                fails[0] = true;
            }

            check[0].SetActive(true);

            if (fails[0])
            {
                check[0].GetComponent<Image>().sprite = goalResult[0];
            }
            else if (!fails[0])
            {
                check[0].GetComponent<Image>().sprite = goalResult[1];
            }
        }

        if (goals.currentGoal == GoalManager.Goals.Third)
        {
            goal[0].SetActive(false);
            goal[1].SetActive(false);
            goal[2].SetActive(true);
            goal[3].SetActive(false);

            if (resources.totalGallonsSold < 500)
            {
                fails[1] = true;
            }

               check[1].SetActive(true);


            if (fails[1])
            {
                check[1].GetComponent<Image>().sprite = goalResult[0];
            }
            else if (!fails[1])
            {
                check[1].GetComponent<Image>().sprite = goalResult[1];
            }
        }

        if (goals.currentGoal == GoalManager.Goals.Final)
        {
            goal[0].SetActive(false);
            goal[1].SetActive(false);
            goal[2].SetActive(false);
            goal[3].SetActive(true);

            if (resources.totalGallonsSold < 1000)
            {
                fails[2] = true;
            }

                     check[2].SetActive(true);


            if (fails[2])
            {
                check[2].GetComponent<Image>().sprite = goalResult[0];
            }
            else if (!fails[2])
            {
                check[2].GetComponent<Image>().sprite = goalResult[1];
            }
        }
    }

    void Awake()
    {
        instance = this;
        resources = ResourceManager.instance;
        goals = GoalManager.instance;
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
        cashTxt.text = "" + newValue.ToString("N0");
    }

    void UpdateOilDisplay(int newValue)
    {
        oilTxt.text = "" + newValue + "/" + resources.storageCapacity;
    }

    void UpdateGallonDisplay(int newValue)
    {
        gallonTxt.text = "" + newValue;
    }

    void UpdatePollutionDisplay(int newValue)
    {
        pollutionTxt.text = "" + newValue;
    }

    void UpdateStorageDisplay(int newValue)
    {
        oilTxt.text = "" + resources.oilAmount + "/" + newValue;
    }

    void UpdateTimeDisplay(int timeElapsed)
    {
        int totalDuration = TimeControls.instance.totalGameTime;
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
        buildingPanelShop.SetActive(false);
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
        buildingPanelShop.SetActive(false);
    }

    public void StorageWarning()
    {

    }
}