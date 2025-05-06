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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Operate();

        if (Input.GetKeyDown(KeyCode.R))
            Repair();

        if (Input.GetKeyDown(KeyCode.T))
            Upgrade();

        if (Input.GetKeyDown(KeyCode.Delete))
            Remove();

        if (currentState == States.Idle && upgraded)
            Operate();
    }

    IEnumerator Extracting()
    {
        yield return new WaitForSeconds(extractionTime);

        if (currentState == States.Broken)
            yield break;

        resources.Oil += oilOutput;
        Condition -= degradationRate;

        if (Condition <= 0)
            Break();
        else
        {
            currentState = States.Idle;

            if (upgraded)
                Operate();
        }
    }

    public override void Operate()
    {
        if (currentState != States.Idle || Condition <= 0)
            return;

        base.Operate();
        operation = StartCoroutine(Extracting());
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

            if (currentState == States.Idle)
                Operate();
        }
    }
}