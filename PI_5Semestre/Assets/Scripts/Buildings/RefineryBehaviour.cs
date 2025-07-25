using UnityEngine;
using System.Collections;

public class RefineryBehaviour : BuildingBase
{
    [Header("Refinery Settings")]
    public float refiningTime;
    public int resourceInput;
    public int gallonOutput;
    public int condition = 100;
    public int Condition
    {
        get { return condition; }
        set { condition = Mathf.Clamp(value, 0, 100); }
    }

    public int degradationRate = 5;
    public int repairCost;
    public int fullRepairCost;
    public int additionalPollution;

    public enum RefineryType { OilBased, PollutionBased }
    public RefineryType refineryType;

    Coroutine operation;

    bool HasEnoughResources()
    {
        return refineryType == RefineryType.OilBased
            ? resources.oilAmount >= resourceInput
            : resources.pollutionAmount >= resourceInput;
    }

    IEnumerator Refining()
    {
        yield return new WaitForSeconds(refiningTime);

        if (currentState == States.Broken)
            yield break;

        if (refineryType == RefineryType.OilBased)
        {
            resources.Pollution(generatedPollution / resources.pollutionModifier);
            resources.Oil(-resourceInput);
        }
        else
        {
            resources.Pollution(-resourceInput);
        }

        resources.Gallons(gallonOutput * resources.productionModifier);
        Condition -= degradationRate;

        if (Condition <= 0)
            Break();
        else
        {
            base.Idle();

            if (upgraded && HasEnoughResources())
                Operate();
        }
    }

    public override void Operate()
    {
        if (currentState != States.Idling || Condition <= 0 || !HasEnoughResources())
            return;

        base.Operate();
        operation = StartCoroutine(Refining());
    }

    public override void Break()
    {
        base.Break();

        if (operation != null)
            StopCoroutine(operation);

        resources.Pollution(additionalPollution / resources.pollutionModifier);
    }

    public override void Repair()
    {
        if (currentState == States.Broken)
        {
            if (resources.cashAmount >= fullRepairCost)
            {
                Condition = 100;
                resources.Cash(-fullRepairCost);
                base.Repair();

                if (upgraded && HasEnoughResources())
                    Operate();
            }
        }
        else if (Condition < 100 && resources.cashAmount >= repairCost)
        {
            Condition = 100;
            resources.Cash(-repairCost);
            base.Repair();
        }
    }

    public override void Upgrade()
    {
        if (resources.cashAmount >= upgradeCost && !upgraded)
        {
            resources.Cash(-upgradeCost);
            base.Upgrade();

            if (currentState == States.Idling && HasEnoughResources())
                Operate();
        }
    }
}