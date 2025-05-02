using UnityEngine;

public class Storage : Buildings
{
    [Header("Storage Settings")]
    public int additionalStorage;

    public override void Start()
    {
        base.Start();

        resources.OilLimit += additionalStorage;
    }

    void OnDestroy()
    {
       resources.OilLimit -= additionalStorage; 
    }
}
