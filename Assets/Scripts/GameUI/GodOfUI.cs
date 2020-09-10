using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodOfUI : MonoBehaviour
{
    Game gameMaster;
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
    //Bonusinfo elements.
    public Text bonusText;
    // Start is called before the first frame update
    void Start()
    {
        floaty = 1f;
        gameMaster = GameObject.Find("GodObject").GetComponent("GodObjectScript") as Game;
    }

    // Update is called once per frame
    void Update()
    {
        //Powerbar update.
        floaty -= .001f;
        fillBar.fillAmount = floaty;
        negBar.fillAmount = (floaty >= -.12f) ? -floaty : .12f;
        //Golfbag update.
        clubText.text = "UP";
        //Holeinfo update.
        holeText.text = "UP";
        parText.text = "UP";
        yardText.text = "UP";
        strokeText.text = gameMaster.GetStrokes().ToString();
        //Windinfo update.
        arrowAnchor.transform.Rotate(Vector3.forward);
        //Strokecounter update.
        bonusText.text = "you can change this!";
    }
}
