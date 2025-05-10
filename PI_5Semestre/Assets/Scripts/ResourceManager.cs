using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("Initial Settings")]
    public int cashAmount = 500000;
    public int Cash
    {
        get { return cashAmount; }
        set { cashAmount = Mathf.Clamp(value, 0, 999999999); }
    }

    public int oilAmount = 0;
    public int Oil
    {
        get { return oilAmount; }
        set { oilAmount = Mathf.Clamp(value, 0, oilLimit); }
    }

    [HideInInspector] public int oilLimit = 0;
    public int OilLimit
    {
        get { return oilLimit; }
        set { oilLimit = Mathf.Clamp(value, 0, 999999999); }
    }

    public int gallonAmount = 0;
    public int Gallons
    {
        get { return gallonAmount; }
        set { gallonAmount = Mathf.Clamp(value, 0, 999999999); }
    }

    public int pollutionAmount = 0;
    public int Pollution
    {
        get { return pollutionAmount; }
        set { pollutionAmount = Mathf.Clamp(value, 0, 1000); }
    }

    [Header("Modifiers")]
    public int productionModifier = 1; 
    public int pollutionModifier = 1;
}