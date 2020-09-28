using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodOfUI : MonoBehaviour
{
    public const string NAME = "UICanvas";

    public Game gameRef;
    public GameController gc;

    // Powerbar elements
    public Image fillBar;
    public Image negBar;
    public Slider marker;
    private float current;
    private float power;

    // Golfbag elements
    public Text clubText;

    // Holeinfo elements
    public Text holeText;
    public Text parText;
    public Text yardText;
    public Text strokeText;

    // WindInfo elements
    public GameObject arrowParent;
    public GameObject arrowTarget;
    private float windSpeed;
    private float windOrient;

    // Bonusinfo elements
    public Text bonusText;
    
    // CamToggleText
    public Text camToggleText;
    public Text normalToggleText;
    
    void Start()
    {
        arrowTarget.transform.parent = arrowParent.transform;
        //devConsole.enabled = false;
    }

    void Update()
    {
        // Return if game is not active
        if (gameRef == null || !gameRef.enabled) return;

        // Powerbar update
        current = (float) gameRef.GetPowerbar().GetCurrent();
        power = (float) gameRef.GetPowerbar().GetPower();
        fillBar.fillAmount = current;
        negBar.fillAmount = (current >= -.12f) ? -current : .12f;
        marker.value = (power == 0) ? current : power;

        // Bag update
        clubText.text = gameRef.GetBag().GetClub().GetName();
        // Holeinfo update
        HoleInfo holeInfo = gameRef.GetHoleInfo();
        HoleData holeData = gameRef.GetHoleBag().GetCurrentHoleData();
        holeText.text = gameRef.GetHoleBag().GetCurrentHoleNumber().ToString();
        parText.text = "Par " + holeInfo.GetPar().ToString();
        yardText.text = MathUtil.ToYardsRounded(holeInfo.GetLength()).ToString() + "y";
        strokeText.text = holeData != null ? holeData.GetStrokes().ToString() : "";

        //Windinfo update
        Vector3 camAngles = gameRef.GetCameraObject().transform.rotation.eulerAngles;
        camAngles[1] = -camAngles[1];
        camAngles[0] = 0;
        camAngles[2] = 0;
        arrowParent.transform.eulerAngles = camAngles;

        // BonusText update
        //bonusText.text = ;
        
        // ToggleText update
        camToggleText.text = Char.ToUpper(gameRef.target.ToString()[0]) + gameRef.target.ToString().ToLower().Substring(1);
        normalToggleText.text = gc.greenNormalMap ? "Angles" : "Normal";
    }
}
