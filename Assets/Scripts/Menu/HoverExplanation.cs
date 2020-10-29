using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverExplanation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text displayable;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        displayable.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void OnPointerEnter(PointerEventData eventData)
    {
        //UnityEngine.Debug.Log("Detected!");
        displayable.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        displayable.gameObject.SetActive(false);
    }
}
