using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodOfUI : MonoBehaviour
{
    //Powerbar elements.
    public Image fillBar;
    public Image negBar;
    public float floaty;
    //Golfbag elements.
    public Text clubText;
    //Holeinfo elements.
    public Text holeText;
    public Text parText;
    public Text yardText;
    public Text strokeText;
    //WindInfo elements.
    public GameObject arrowAnchor;

    //Strokecounter elements.

    // Start is called before the first frame update
    void Start()
    {
        floaty = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Powerbar update.
        floaty -= .001f;
        fillBar.fillAmount = floaty;
        negBar.fillAmount = -floaty;
        //Golfbag update.
        clubText.text = "UP";
        //Holeinfo update.
        holeText.text = "UP";
        parText.text = "UP";
        yardText.text = "UP";
        //Windinfo update.
        arrowAnchor.transform.Rotate(Vector3.forward);
        //Strokecounter update.
    }
}
