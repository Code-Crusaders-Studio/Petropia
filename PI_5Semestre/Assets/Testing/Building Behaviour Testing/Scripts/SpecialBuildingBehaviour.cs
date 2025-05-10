using UnityEngine;

public class SpecialBuildingBehaviour : BaseBuilding
{
    [Header("Special Settings")]
    public int buffModifier;
    int defaultModifier = 1;

    public override void Build()
    {
        base.Build();
        ApplyBuff();
    }

    public override void Remove()
    {
        RemoveBuff();
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

    void RemoveBuff()
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