using UnityEngine;

public class ShowShopManager : MonoBehaviour
{
    [SerializeField] private GameObject extractorList, storageList, refineryList, portList, specialList;

    public void ShowExtractor()
    {
        extractorList.gameObject.SetActive(true);
        storageList.gameObject.SetActive(false);
        refineryList.gameObject.SetActive(false);
        portList.gameObject.SetActive(false);
        specialList.gameObject.SetActive(false);

    }

    public void ShowStorage()
    {
        extractorList.gameObject.SetActive(false);
        storageList.gameObject.SetActive(true);
        refineryList.gameObject.SetActive(false);
        portList.gameObject.SetActive(false);
        specialList.gameObject.SetActive(false);

    }

    public void ShowRefinery()
    {
        extractorList.gameObject.SetActive(false);
        storageList.gameObject.SetActive(false);
        refineryList.gameObject.SetActive(true);
        portList.gameObject.SetActive(false);
        specialList.gameObject.SetActive(false);

    }

    public void ShowPort()
    {
        extractorList.gameObject.SetActive(false);
        storageList.gameObject.SetActive(false);
        refineryList.gameObject.SetActive(false);
        portList.gameObject.SetActive(true);
        specialList.gameObject.SetActive(false);

    }

    public void ShowSpecial()
    {
        extractorList.gameObject.SetActive(false);
        storageList.gameObject.SetActive(false);
        refineryList.gameObject.SetActive(false);
        portList.gameObject.SetActive(false);
        specialList.gameObject.SetActive(true);

    }
}
