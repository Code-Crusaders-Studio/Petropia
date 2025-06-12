using UnityEngine;

public class SpecialBuildingBehaviour : BuildingBase
{
    [Header("Special Settings")]
    public int buffModifier;
    int defaultModifier = 1;

    public override void Start()
    {
        base.Start();
        ApplyBuff();
    }

    public override void Remove()
    {
        RevertBuff();
        base.Remove();
    }

    void ApplyBuff()
    {
        switch (label)
        {
            case BuildingLabels.Research:
                resources.pollutionModifier = buffModifier;
                break;

            case BuildingLabels.Capacitation:
                resources.productionModifier = buffModifier;
                break;
        }
    }

    void RevertBuff()
    {
        switch (label)
        {
            case BuildingLabels.Research:
                resources.pollutionModifier = defaultModifier;
                break;

            case BuildingLabels.Capacitation:
                resources.productionModifier = defaultModifier;
                break;
        }
    }
}