using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    protected ResourceManager resources;

    [Header("General Settings")]
    public string id;
    public BuildingLabels label;
    public enum BuildingLabels
    {
        LandConv,
        LandSust,
        OffshoreConv,
        OffshoreSust,
        TankSmall,
        TankMed,
        TankBig,
        RefConv,
        RefSust,
        ResiProc,
        Port,
        Research,
        Capacitation
    }

    public string terrain;
    public int buildCost;
    public int pollution;
    public int upgradeCost;
    public bool upgraded;

    public States currentState = States.Idle;
    public enum States
    {
        Idle,
        Operating,
        Broken
    }

    void Start()
    {
        resources = GameObject.FindWithTag("GameController").GetComponent<ResourceManager>();

        Build();
    }

    void UpdateID()
    {
        id = $"{label}{(upgraded ? "1" : "0")}{currentState}";

        Debug.Log(id);
    }

    // Métodos comuns para todas as estruturas
    public virtual void Build()
    {
        if (resources.Cash >= buildCost)
        {
            resources.Cash -= buildCost;
            resources.Pollution += pollution / resources.pollutionModifier;
            UpdateID();

            Debug.Log(id + " built");
        }
    }

    public virtual void Remove()
    {
        Destroy(transform.parent.gameObject);

        Debug.Log(id + " removed");
    }

    // Métodos opcionais para estruturas específicas
    public virtual void Operate()
    {
        currentState = States.Operating;
        UpdateID();

        Debug.Log(id + " is operating");
    }

    public virtual void Break()
    {
        currentState = States.Broken;
        UpdateID();

        Debug.Log(id + " broke");
    }

    public virtual void Repair()
    {
        currentState = States.Idle;
        UpdateID();

        Debug.Log(id + " was repaired");
    }

    public virtual void Upgrade()
    {
        upgraded = true;
        UpdateID();

        Debug.Log(id + " was upgraded");
    }

    public bool IsOperational()
    {
        return label == BuildingLabels.LandConv ||
       label == BuildingLabels.LandSust ||
       label == BuildingLabels.OffshoreConv ||
       label == BuildingLabels.OffshoreSust ||
       label == BuildingLabels.RefConv ||
       label == BuildingLabels.RefSust ||
       label == BuildingLabels.ResiProc ||
       label == BuildingLabels.Port;
    }

    public bool IsUpgradable()
    {
        return label == BuildingLabels.LandConv || 
       label == BuildingLabels.LandSust || 
       label == BuildingLabels.OffshoreConv || 
       label == BuildingLabels.OffshoreSust || 
       label == BuildingLabels.RefConv || 
       label == BuildingLabels.RefSust || 
       label == BuildingLabels.ResiProc;
    }
}