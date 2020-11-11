using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public RawImage mySelector;
    public RawImage notMySelector;
	public bool left;
	public bool right;
	public ItemSelector itemSelector;

    void Start() { }
    void Update() { }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mySelector.gameObject.SetActive(true);
        notMySelector.gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData) { }
	
	public void OnPointerClick(PointerEventData pointerEventData)
    {
		itemSelector.clickSelect(left, right);
    }
}
