using UnityEngine;

public class StorageBehaviour : BuildingBase
{
    [Header("Storage Settings")]
    public int additionalStorage;

    public override void Start()
    {
        base.Start();
        IncreaseCapacity();
    }

    public override void OnDestroy()
    {
        DecreaseCapacity();
        base.OnDestroy();
    }

    void IncreaseCapacity()
    {
        resources.Storage(additionalStorage);
    }

    void DecreaseCapacity()
    {
        resources.Storage(-additionalStorage);

        if (resources.oilAmount > resources.storageCapacity)
        {
            int excess = resources.oilAmount - resources.storageCapacity;
            resources.Oil(-excess);
        }
    }
}