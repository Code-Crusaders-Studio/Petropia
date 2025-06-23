using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject structurePrefOffshore, structurePrefLand;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateStructure(structurePrefOffshore);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CreateStructure(structurePrefLand);
        }
    }

    void CreateStructure(GameObject prefObject)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitMouse;

        if (Physics.Raycast(ray, out hitMouse))
        {
            GameObject collObj = hitMouse.collider.gameObject;

            if (collObj.layer == prefObject.layer && !collObj.CompareTag(prefObject.tag))
            {
                Vector3 posStructure = new Vector3(collObj.transform.position.x, collObj.transform.position.y + 1, collObj.transform.position.z);

                Instantiate(prefObject, posStructure, Quaternion.identity);
            }
        }
    }
}
