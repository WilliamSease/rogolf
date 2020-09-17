using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodOfUI : MonoBehaviour
{
    public Game gameRef;
    public GameController gc;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRef == null) return;
        //Powerbar update.
        floaty = (float) gameRef.GetPowerbar().GetCurrent();
        fillBar.fillAmount = floaty;
        negBar.fillAmount = (floaty >= -.12f) ? -floaty : .12f;
        //Bag update
        clubText.text = gameRef.GetBag().GetClub().GetName();
        //Holeinfo update.
        HoleInfo holeInfo = gameRef.GetHoleInfo();
        holeText.text = holeInfo.GetHoleNumber().ToString();
        parText.text = "Par " + holeInfo.GetPar().ToString();
        yardText.text = holeInfo.GetYardsRounded().ToString() + "y";
        strokeText.text = gameRef.GetStrokes().ToString();
        //Windinfo update.
        arrowAnchor.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        //BonusText update.
        //bonusText.text = ;
    }
}
