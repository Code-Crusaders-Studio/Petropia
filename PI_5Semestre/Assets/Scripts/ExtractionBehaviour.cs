using UnityEngine;
using System.Collections;

public class ExtractionBehaviour : BuildingBase
{
    [Header("Extraction Settings")]
    public float extractionTime;
    public int oilOutput;
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

    Coroutine operation;

    IEnumerator Extracting()
    {
        yield return new WaitForSeconds(extractionTime);

        if (currentState == States.Broken)
            yield break;

        resources.Pollution(generatedPollution / resources.pollutionModifier);
        resources.Oil(oilOutput * resources.productionModifier);

        Condition -= degradationRate;

        if (Condition <= 0)
            Break();
        else
        {
            base.Idle();

            if (upgraded)
                Operate();
        }
    }

    public override void Operate()
    {
        if (currentState != States.Idling || Condition <= 0)
            return;

        base.Operate();
        operation = StartCoroutine(Extracting());
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

                if (upgraded)
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

            if (currentState == States.Idling)
                Operate();
        }
    }
}