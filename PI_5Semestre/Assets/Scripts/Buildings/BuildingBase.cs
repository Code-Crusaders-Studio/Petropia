using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    protected ResourceManager resources;

    [Header("General Settings")]
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

    public string buildingName;
    public string buildingDescription;
    public string terrain;
    public int buildCost;
    public int generatedPollution;
    public int upgradeCost;
    public bool upgraded;

    public States currentState = States.Idling;
    public enum States
    {
        Idling,
        Operating,
        Broken
    }

    public virtual void Start()
    {
        resources = ResourceManager.instance;

        if (resources.cashAmount >= buildCost)
        {
            resources.Cash(-buildCost);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }

    public virtual void OnDestroy()
    {
        Debug.Log(label + " removed");
    }

    // Funções específicas
    public virtual void Idle()
    {
        currentState = States.Idling;
        Debug.Log(label + " is idling");
    }

    public virtual void Operate()
    {
        currentState = States.Operating;
        Debug.Log(label + " is operating");
    }

    public virtual void Break()
    {
        currentState = States.Broken;
        Debug.Log(label + " broke");
    }

    public virtual void Repair()
    {
        Idle();
        Debug.Log(label + " was repaired");
    }

    public virtual void Upgrade()
    {
        upgraded = true;
        Debug.Log(label + " was upgraded");
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

    public bool CanBeRepaired()
    {
        return IsOperational() && label != BuildingLabels.Port;
    }

    public bool IsUpgradable()
    {
        return !upgraded && (
            label == BuildingLabels.LandConv ||
            label == BuildingLabels.LandSust ||
            label == BuildingLabels.OffshoreConv ||
            label == BuildingLabels.OffshoreSust ||
            label == BuildingLabels.RefConv ||
            label == BuildingLabels.RefSust ||
            label == BuildingLabels.ResiProc
        );
    }
}