using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject toShow;
    
    public void OnPointerEnter(PointerEventData eventData) { toShow.gameObject.SetActive(true); }

    public void OnPointerExit(PointerEventData eventData) { toShow.gameObject.SetActive(false); }
}
