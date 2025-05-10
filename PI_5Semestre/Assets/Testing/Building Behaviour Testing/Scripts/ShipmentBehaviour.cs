using System.Collections;
using UnityEngine;

public class ShipmentBehaviour : BaseBuilding
{
    [Header("Shipment Settings")]
    public float shippingTime;
    public int shippingCost;
    public int cashPerGallon;
    public int maxGallonsPerShipment;

    Coroutine operation;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            Operate();

        if (Input.GetKeyDown(KeyCode.Delete))
            Remove();
    }

    bool CanShip()
    {
        return resources.Cash >= shippingCost &&
               resources.Gallons > 0;
    }

    IEnumerator Shipping()
    {
        int gallonsToSell = Mathf.Min(resources.Gallons, maxGallonsPerShipment);

        resources.Cash -= shippingCost;
        resources.Gallons -= gallonsToSell;

        yield return new WaitForSeconds(shippingTime);

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

    public override void Remove()
    {
        resources.Pollution -= pollution;
        base.Remove();
    }
}