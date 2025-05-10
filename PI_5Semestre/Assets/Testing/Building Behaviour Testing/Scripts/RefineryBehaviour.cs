using UnityEngine;
using System.Collections;

public class RefineryBehaviour : BaseBuilding
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Operate();

        if (Input.GetKeyDown(KeyCode.A))
            Repair();

        if (Input.GetKeyDown(KeyCode.Z))
            Upgrade();

        if (Input.GetKeyDown(KeyCode.Delete))
            Remove();

        if (currentState == States.Idle && upgraded && HasEnoughResources())
            Operate();
    }

    bool HasEnoughResources()
    {
        return refineryType == RefineryType.OilBased
            ? resources.Oil >= resourceInput
            : resources.Pollution >= resourceInput;
    }

    IEnumerator Refining()
    {
        yield return new WaitForSeconds(refiningTime);

        if (currentState == States.Broken)
            yield break;

        if (refineryType == RefineryType.OilBased)
        {
            resources.Oil -= resourceInput;
        }
        else
        {
            resources.Pollution -= resourceInput;
        }

        resources.Gallons += gallonOutput * resources.productionModifier;
        Condition -= degradationRate;

        if (Condition <= 0)
            Break();
        else
        {
            currentState = States.Idle;

            if (upgraded && HasEnoughResources())
                Operate();
        }
    }

    public override void Operate()
    {
        if (currentState != States.Idle || Condition <= 0 || !HasEnoughResources())
            return;

        base.Operate();
        operation = StartCoroutine(Refining());
    }

    public override void Break()
    {
        base.Break();

        if (operation != null)
            StopCoroutine(operation);

        resources.Pollution += additionalPollution;
    }

    public override void Repair()
    {
        if (currentState == States.Broken)
        {
            if (resources.Cash >= fullRepairCost)
            {
                Condition = 100;
                resources.Cash -= fullRepairCost;
                resources.Pollution -= additionalPollution;
                base.Repair();
            }
        }
        else if (Condition < 100 && resources.Cash >= repairCost)
        {
            Condition = 100;
            resources.Cash -= repairCost;
            base.Repair();
        }
    }

    public override void Upgrade()
    {
        if (resources.Cash >= upgradeCost && !upgraded)
        {
            resources.Cash -= upgradeCost;
            base.Upgrade();

            if (currentState == States.Idle && HasEnoughResources())
                Operate();
        }
    }

    public override void Remove()
    {
        Repair();
        resources.Pollution -= pollution;
        base.Remove();
    }
}