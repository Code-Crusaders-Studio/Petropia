using UnityEngine;

public class Buildings : MonoBehaviour
{
    public string id, placementSite;
    public int cost, polution, degradation, level;
    public float operationTime;
    public enum States { Idle, Operating, Broken };
    public States currentState;
    public bool isOperating, isBroken;

    protected GameObject globalController;
    protected ResourceManager resources;

    public void FindManager()
    {
        globalController = GameObject.FindWithTag("GameController");
        resources = globalController.GetComponent<ResourceManager>();

        Debug.Log(resources + "found");
    }

    public void Build(int cost, int polution, string placementSite)
    {
        resources.cashAmount -= cost;
        resources.polutionAmount += polution;

        Debug.Log("cashAmount = " + resources.cashAmount + "\npolutionAmount = " + resources.polutionAmount + "\nextractor placed");
    }

    public void Operate()
    {
        if (currentState == States.Idle)
            currentState = States.Operating;
    }

    public void Break()
    {
        currentState = States.Broken;
    }

    public void Repair(int cost)
    {
        if(currentState == States.Broken)
        {
            cost -= resources.cashAmount;
        }
    }

    public void Upgrade(int cost)
    {
        cost -= resources.cashAmount;
        level = 2;
    }
}