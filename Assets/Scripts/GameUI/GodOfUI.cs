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
    public Slider marker;
    private float current;
    private float power;
    //Golfbag elements.
    public Text clubText;
    //Holeinfo elements.
    public Text holeText;
    public Text parText;
    public Text yardText;
    public Text strokeText;
    //WindInfo elements.
    public GameObject arrowParent;
    public GameObject arrowTarget;
    private float windSpeed;
    private float windOrient;
    //Bonusinfo elements.
    public Text bonusText;
	//CamToggleText
	public Text camToggleText;
	public Text normalToggleText;
    // Start is called before the first frame update
    
    void Start()
    {
        arrowTarget.transform.parent = arrowParent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRef == null) return;
        //Powerbar update.
        current = (float) gameRef.GetPowerbar().GetCurrent();
        power = (float) gameRef.GetPowerbar().GetPower();
        fillBar.fillAmount = current;
        negBar.fillAmount = (current >= -.12f) ? -current : .12f;
        marker.value = (power == 0) ? current : power;

        //Bag update
        clubText.text = gameRef.GetBag().GetClub().GetName();
        //Holeinfo update.
        HoleInfo holeInfo = gameRef.GetHoleInfo();
        holeText.text = holeInfo.GetHoleNumber().ToString();
        parText.text = "Par " + holeInfo.GetPar().ToString();
        yardText.text = holeInfo.GetYardsRounded().ToString() + "y";
        strokeText.text = gameRef.GetStrokes().ToString();
        //Windinfo update.
        GameObject cam = gc.camera;
		Vector3 camAngles = cam.transform.rotation.eulerAngles;
		camAngles[1] = -camAngles[1];
		camAngles[0] = 0;
		camAngles[2] = 0;
        arrowParent.transform.eulerAngles = camAngles;
        //BonusText update.
        //bonusText.text = ;
		//ToggleText update.
		camToggleText.text = Char.ToUpper(gameRef.target.ToString()[0]) + gameRef.target.ToString().ToLower().Substring(1);
		if (gc.greenNormalMap)	
			normalToggleText.text = "Angles";
		else
			normalToggleText.text = "Normal";
		
    }
}
