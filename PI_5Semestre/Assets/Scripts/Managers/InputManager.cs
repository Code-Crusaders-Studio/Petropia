using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public GameObject curPref;

    [SerializeField] private float holdTime = 0.5f;

    private bool isHolding;
    private float timeMouseDown;
    private Camera cam;

    BuildingBase building;

    private void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        {
            CreateStructure();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.CompareTag("Structures"))
                {
                    timeMouseDown = Time.time;
                    isHolding = false;
                    StartCoroutine(CheckHold(hit.collider.gameObject));
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - timeMouseDown < holdTime && !isHolding)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.gameObject.CompareTag("Structures"))
                {
                    OnQuickTap(hit.collider.gameObject);
                }
            }
            StopAllCoroutines();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Atalho para alto contraste
            GameSettings.highContrast = !GameSettings.highContrast;
    }

    public void CreateStructure()
    {
        if (curPref != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitMouse;

            if (Physics.Raycast(ray, out hitMouse))
            {
                GameObject collObj = hitMouse.collider.gameObject;

                if (collObj.layer == curPref.layer && !collObj.CompareTag(curPref.tag))
                {
                    Vector3 posStructure = new Vector3(collObj.transform.position.x, collObj.transform.position.y + 1, collObj.transform.position.z);

                    Instantiate(curPref, posStructure, Quaternion.identity);
                    curPref = null;
                }
            }
        }
        else
        {
            Debug.Log($"Compre alguma estrutura na loja");
        }
    }

    private IEnumerator CheckHold(GameObject hitObject)
    {
        yield return new WaitForSeconds(holdTime);
        isHolding = true;
        OnLongPress(hitObject);
    }

    private void OnQuickTap(GameObject hitObject)
    {
        Debug.Log("Toque rápido no objeto!");
        building = hitObject.GetComponentInChildren<BuildingBase>();

        if (building != null && building.IsOperational())
        {
            building.Operate();
        }
    }

    private void OnLongPress(GameObject hitObject)
    {
        Debug.Log("Pressão longa no objeto!");
        building = hitObject.GetComponentInChildren<BuildingBase>();

        if (building != null)
            GameUI.instance.OpenBuildingPanel(building, hitObject, building.buildingName, building.buildingDescription);
    }
}