using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public RawImage leftSelector;
    public RawImage rightSelector;
    // Start is called before the first frame update
    void Start()
    {
        leftSelector.gameObject.SetActive(false);
        rightSelector.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftSelector.gameObject.SetActive(true);
            rightSelector.gameObject.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightSelector.gameObject.SetActive(true);
            leftSelector.gameObject.SetActive(false);
        }
    }
}
