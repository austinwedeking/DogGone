using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        tooltip.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Debug.Log("mouse is over");
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Debug.Log("mouse is not over");
        tooltip.SetActive(false);
    }
}
