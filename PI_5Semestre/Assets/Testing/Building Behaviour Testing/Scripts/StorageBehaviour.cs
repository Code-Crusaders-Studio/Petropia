using UnityEngine;

public class StorageBehaviour : BaseBuilding
{
    [Header("Storage Settings")]
    public int additionalStorage;

    public override void Build()
    {
        base.Build();
        IncreaseCapacity();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
            Remove();
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

    public override void Remove()
    {
        DecreaseCapacity();
        base.Remove();
    }
}