using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    // Start is called before the first frame update
    private int toGo = 0;
    private int itr = 0;
    public Canvas thisCanvas;
    public RawImage loadingImage;
    public Text loadingText;
    
    void Start()
    {
        loadingImage.transform.localScale = new Vector3(1, 1, 1);
        loadingText.transform.localScale = new Vector3(1, 1, 1);
        Invoke(200);
    }

    // Update is called once per frame
    void Update()
    {
        if(!thisCanvas.enabled) return;
        if (itr > toGo)
            Disable();
        else itr++;
        loadingText.text = itr + "/" + toGo;
    }
    
    void Invoke(int frames)
    {
        Enable();
        toGo = frames;
    }
    
    void Enable() { thisCanvas.enabled = true; }
    void Disable() { thisCanvas.enabled = false; }
}
