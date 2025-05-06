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

    void OnDestroy()
    {
        DecreaseCapacity();
    }
}