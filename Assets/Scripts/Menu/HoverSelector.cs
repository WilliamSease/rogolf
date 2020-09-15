using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RawImage mySelector;
    public RawImage notMySelector;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mySelector.gameObject.SetActive(true);
        notMySelector.gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
