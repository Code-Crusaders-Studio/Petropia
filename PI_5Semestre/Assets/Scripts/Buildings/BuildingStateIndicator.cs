using UnityEngine;

public class BuildingStateIndicator : MonoBehaviour
{
    public BuildingBase building;
    SpriteRenderer sr;
    public Sprite[] stateSprites;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);

        switch (building.currentState)
        {
            case BuildingBase.States.Idling:
                sr.sprite = stateSprites[0];
                break;
            case BuildingBase.States.Operating:
                sr.sprite = stateSprites[1];
                break;
            case BuildingBase.States.Broken:
                sr.sprite = stateSprites[2];
                break;
        }
    }
}
