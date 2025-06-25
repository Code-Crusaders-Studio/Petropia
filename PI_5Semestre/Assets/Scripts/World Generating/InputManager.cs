using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public GameObject curPref;

    void Update()
    {
        if (Input.touchCount == 1 || Input.GetMouseButtonDown(0))
        {
            CreateStructure();
        }
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
}
