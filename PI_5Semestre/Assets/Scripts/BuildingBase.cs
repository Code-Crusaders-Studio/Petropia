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
        resources = GameObject.FindWithTag("GameController").GetComponent<ResourceManager>();

        if (resources.cashAmount >= buildCost)
        {
            resources.Cash(-buildCost);
        }
    }

    public virtual void OnDestroy()
    {
        Debug.Log(id + " removed");
    }

    // Funções específicas
    public virtual void Idle()
    {
        currentState = States.Idling;
        Debug.Log(id + " is idling");
    }

    public virtual void Operate()
    {
        currentState = States.Operating;
        Debug.Log(id + " is operating");
    }

    public virtual void Break()
    {
        currentState = States.Broken;
        Debug.Log(id + " broke");
    }

    public virtual void Repair()
    {
        Idle();
        Debug.Log(id + " was repaired");
    }

    public virtual void Upgrade()
    {
        upgraded = true;
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