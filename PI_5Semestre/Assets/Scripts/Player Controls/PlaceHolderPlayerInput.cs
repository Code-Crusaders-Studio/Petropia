using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceHolderPlayerInput : MonoBehaviour
{
    Camera mainCamera;
    GameObject currentPressedObject;
    bool isPressingOnObject = false;
    float pressingDuration = 0.5f, currentPressingTime = 0f;

    BuildingBase building;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Mouse mouse = Mouse.current;
        Vector3 mousePos = mouse.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePos);

        bool isMouseOverObject = Physics.Raycast(ray, out RaycastHit hit);
        GameObject hitObject = isMouseOverObject ? hit.collider.gameObject : null;

        if (mouse.leftButton.wasReleasedThisFrame && isMouseOverObject)
        {
            building = hitObject.GetComponentInChildren<BuildingBase>();

            if (building != null && building.IsOperational())
            {
                building.Operate();
            }
        }

        if (mouse.leftButton.isPressed)
        {
            if (isMouseOverObject)
            {
                if (currentPressedObject == hitObject)
                {
                    currentPressingTime += Time.deltaTime;

                    if (currentPressingTime >= pressingDuration && !isPressingOnObject)
                    {
                        isPressingOnObject = true;

                        if (building != null)
                            GameUI.instance.OpenBuildingPanel(building, hitObject, hitObject.name, building.description);
                    }
                }
                else
                {
                    currentPressedObject = hitObject;
                    currentPressingTime = 0f;
                    isPressingOnObject = false;
                }
            }
            else
            {
                Reset();
            }
        }
        else
        {
            Reset();
        }
    }

    private void Reset()
    {
        currentPressedObject = null;
        currentPressingTime = 0f;
        isPressingOnObject = false;
    }
}