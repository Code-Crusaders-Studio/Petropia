using UnityEngine;

public class StorageBehaviour : BuildingBase
{
    [Header("Storage Settings")]
    public int additionalStorage;

    public override void Build()
    {
        base.Build();
        IncreaseCapacity();
    }

    public override void Remove()
    {
        DecreaseCapacity();
        base.Remove();
    }

    void IncreaseCapacity()
    {
        resources.OilLimit += additionalStorage;
    }

    void DecreaseCapacity()
    {
        resources.OilLimit -= additionalStorage;

        if (resources.Oil > resources.OilLimit)
            resources.Oil = resources.OilLimit;
    }
}