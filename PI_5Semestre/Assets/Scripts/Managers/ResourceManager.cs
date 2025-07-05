using System;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    [Header("Initial Settings")]
    public int cashAmount = 500000;
    public int oilAmount = 0;
    public int gallonAmount = 0;
    public int pollutionAmount = 0;

    [HideInInspector] public int storageCapacity = 0;
    [HideInInspector] public int totalCashEarned, totalOilExtracted, totalGallonsProduced, totalGallonsSold, totalPollutionCaused;

    [Header("Modifiers")]
    public int productionModifier = 1;
    public int pollutionModifier = 1;

    public event Action<int> OnCashChanged, OnOilChanged, OnGallonsChanged, OnPollutionChanged, OnStorageChanged;

    void Awake() => instance = this;

    ///

    public Image gallonGoalBar;

    ///

    public void Cash(int amount)
    {
        cashAmount = Mathf.Clamp(cashAmount + amount, 0, 999999999);

        if (amount > 0)
            totalCashEarned += amount;

        OnCashChanged?.Invoke(cashAmount);
    }

    public void Oil(int amount)
    {
        oilAmount = Mathf.Clamp(oilAmount + amount, 0, storageCapacity);

        if (amount > 0)
            totalOilExtracted += amount;

        if (oilAmount == storageCapacity)
        {
            GameUI.instance.StorageWarning();
            Debug.Log("storage full");
        }

        OnOilChanged?.Invoke(oilAmount);
    }

    public void Gallons(int amount)
    {
        gallonAmount = Mathf.Clamp(gallonAmount + amount, 0, 500000);

        if (amount > 0)
            totalGallonsProduced += amount;
        else if (amount < 0)
        {
            totalGallonsSold += -amount;
            gallonGoalBar.fillAmount = totalGallonsSold / 2000f;
        }
         

        OnGallonsChanged?.Invoke(gallonAmount);
    }

    public void Pollution(int amount)
    {
        pollutionAmount = Mathf.Clamp(pollutionAmount + amount, 0, 1000);

        if (amount > 0)
            totalPollutionCaused += amount;

        if (pollutionAmount >= 1000)
            GameManager.instance.OnGameOver();

        OnPollutionChanged?.Invoke(pollutionAmount);
    }

    public void Storage(int amount)
    {
        storageCapacity = Mathf.Clamp(storageCapacity + amount, 0, 1000000);
        OnStorageChanged?.Invoke(storageCapacity);
    }
}