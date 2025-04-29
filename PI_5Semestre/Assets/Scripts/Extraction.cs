using System.Collections;
using UnityEngine;

public class Extraction : Buildings
{
    void Awake()
    {
        FindManager();
    }

    void Start()
    {
        Build(cost, polution, placementSite);
    }

    void Update()
    {
        switch (currentState)
        {
            case States.Idle:
                polution = 50;

                break;

            case States.Operating:
                if (level == 1)
                    StartCoroutine(ExtractLevel1(operationTime));
                else if (level == 2)
                    StartCoroutine(ExtractLevel2(operationTime));

                break;

            case States.Broken:
                polution = 100;

                break;
        }

        if (Input.GetKeyDown(KeyCode.E))
            Operate();

        if (!isBroken && degradation <= 0)
            Break();
    }

    IEnumerator ExtractLevel1(float operationTime)
    {
        yield return new WaitForSeconds(operationTime);

        resources.oilAmount += 50;
        degradation -= 5;

        Debug.Log("Level 1 Extraction Done");

        currentState = States.Idle;
        isOperating = false;

        Debug.Log("oilAmount = " + resources.oilAmount + "\ndegradation = " + degradation + "\ncurrentState = " + currentState);
    }

    IEnumerator ExtractLevel2(float operationTime)
    {
        while (currentState == States.Operating)
        {
            yield return new WaitForSeconds(operationTime);

            resources.oilAmount += 50;
            degradation -= 5;

            Debug.Log("Level 2 Continuous Extraction");

            if (degradation <= 0 || isBroken)
            {
                Break();
                yield break;
            }
        }

        isOperating = false;
    }
}