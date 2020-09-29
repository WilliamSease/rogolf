using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAllFonts : MonoBehaviour
{
	static bool done = false;
    public Font myFont;
    // Start is called before the first frame update
    void Start()
    {
		if (done) return;
        Text[] textComponents = Component.FindObjectsOfType<Text>();
        foreach (Text component in textComponents)
        {
            component.font = myFont;
            component.fontSize = component.fontSize + 5;
        }
		done = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
