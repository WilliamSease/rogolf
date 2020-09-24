using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddOutlines : MonoBehaviour
{
	public Canvas og;
    // Start is called before the first frame update
    void Start()
    {
        Text[] textComponents = og.GetComponentsInChildren<Text>();
        foreach (Text component in textComponents)
        {
			Outline o = component.gameObject.AddComponent<Outline>();
			o.effectDistance = new Vector2(2.5f,2.5f);
			component.color = Color.white;
			
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
