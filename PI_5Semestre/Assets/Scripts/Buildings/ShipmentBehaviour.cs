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
        return resources.cashAmount >= shippingCost &&
               resources.gallonAmount > 0;
    }

    IEnumerator Shipping()
    {
        int gallonsToSell = Mathf.Min(resources.gallonAmount, maxGallonsPerShipment);

        resources.Cash(-shippingCost);
        resources.Gallons(-gallonsToSell);

        yield return new WaitForSeconds(shippingTime);

        resources.Pollution(generatedPollution / resources.pollutionModifier);
        resources.Cash(gallonsToSell * cashPerGallon);
        base.Idle();
    }

    public override void Operate()
    {
        if (currentState != States.Idling || !CanShip())
            return;

        base.Operate();
        operation = StartCoroutine(Shipping());
    }
}