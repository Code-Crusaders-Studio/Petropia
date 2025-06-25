using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ShopManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float holdDuration = 0.5f;

    [SerializeField] private GameObject inputManagerObj;

    [SerializeField] private GameObject curStructure;

    private Coroutine holdCoroutine;
    private bool wasHeld;

    public void OnPointerDown(PointerEventData eventData)
    {
        wasHeld = false;
        holdCoroutine = StartCoroutine(CheckHold());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (holdCoroutine != null)
        {
            StopCoroutine(holdCoroutine);
            holdCoroutine = null;
        }

        if (!wasHeld)
        {
            OnQuickClick();
        }
    }

    private IEnumerator CheckHold()
    {
        yield return new WaitForSeconds(holdDuration);
        wasHeld = true;
        OnLongPress();
    }

    private void OnQuickClick()
    {
        Debug.Log($"{curStructure.name} selecionada. Pode posicioná-la");
        inputManagerObj.GetComponent<InputManager>().curPref = curStructure;
    }

    private void OnLongPress()
    {
        Debug.Log($"Descrição da {curStructure.name}");
    }
}
