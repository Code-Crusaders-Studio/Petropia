using System.Collections;
using UnityEngine;

public class ShipmentBehaviour : BuildingBase
{
    [Header("Shipment Settings")]
    public float shippingTime;
    public int shippingCost;
    public int cashPerGallon;
    public int maxGallonsPerShipment;

    Coroutine operation;

    bool CanShip()
    {
        return resources.Cash >= shippingCost &&
               resources.Gallons > 0;
    }

    IEnumerator Shipping()
    {
        int gallonsToSell = Mathf.Min(resources.Gallons, maxGallonsPerShipment);

        resources.Cash -= shippingCost;
        resources.totalGallonsSold += gallonsToSell;
        resources.Gallons -= gallonsToSell;

        yield return new WaitForSeconds(shippingTime);

        resources.Pollution += generatedPollution / resources.pollutionModifier;
        resources.Cash += gallonsToSell * cashPerGallon;
        currentState = States.Idle;
    }

    public override void Operate()
    {
        if (currentState != States.Idle || !CanShip())
            return;

        base.Operate();
        operation = StartCoroutine(Shipping());
    }
}