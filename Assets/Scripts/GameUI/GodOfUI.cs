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
	public GameObject powerbarParent;
    public Image fillBar;
    public Image negBar;
    public Slider marker;
    private float current;
    private float power;
	public Text[] underText = new Text[4];
	public bool renderPowerbar = true;
	
	//Distance Display Elements
	public GameObject distanceDisplay;
	public Text distanceText;

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
	public Text windText;
	public Camera windCamera;
	
	//LieInfo elements
	public Text lieText;

    // Bonusinfo elements
    public Text bonusText;
	
	// Playerinfo elements
	public Text[] playerinfoText = new Text[4];
    
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
		if (!renderPowerbar)
		{
			powerbarParent.SetActive(false);
			distanceDisplay.SetActive(true);
			distanceText.text = MathUtil.ToYards(gameRef.GetCurrentDistance().GetCurrentDistance()).ToString("F2") + "y";

		}
		else
		{
			powerbarParent.SetActive(true);
			distanceDisplay.SetActive(false);
		}
        // Powerbar update. Do this every frame. Rest can be done whenever
        current = (float) gameRef.GetPowerbar().GetCurrent();
        power = (float) gameRef.GetPowerbar().GetPower();
        fillBar.fillAmount = current;
        negBar.fillAmount = (current >= -.12f) ? -current : .12f;
        marker.value = (power == 0) ? current : power;
		
		int maxVal = (int) MathUtil.ToYardsRounded(gameRef.GetBag().GetClub().GetDistance());
		underText[0].text = (int)((float)maxVal) + "y";
		underText[1].text = (int)((float)maxVal * .75f) + "y";
		underText[2].text = (int)((float)maxVal * .5f) + "y";
		underText[3].text = (int)((float)maxVal * .25f) + "y";

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
		Wind tWind = gameRef.GetWind();
        Vector3 camAngles = gameRef.GetCameraObject().transform.rotation.eulerAngles;
        camAngles[1] = -camAngles[1] + MathUtil.RadsToDeg((float) tWind.GetAngle());//This is wrong. lord help us
        camAngles[0] = 0;
        arrowParent.transform.eulerAngles = camAngles;
		/*transform.RotateAround(arrowParent.transform.position, 
			windCamera.transform.right, 
				gameRef.GetCameraObject().transform.eulerAngles.x);*/
		windText.text = "" + tWind.GetSpeed().ToString().Substring(0, Math.Min(3, tWind.GetSpeed().ToString().Length)) + "m";
		
		//Lieinfo update
		float[] vals = gameRef.GetTerrainAttributes().GetTeeTerrain().GetBounds();
		lieText.text = floatToPct(vals[0].ToString()) + "%~" + 
			floatToPct(vals[1].ToString()) + "%";
        // BonusText update
		List<Item> heldItems = gameRef.GetItemBag().GetHeldItems();
		bonusText.text = "";
		foreach (Item i in heldItems)
		{
			bonusText.text = bonusText.text + i.GetName() + " -> " + i.GetDescription() + "\n";
		}
		
		// Playerstats update
		PlayerAttributes plr = gameRef.GetPlayerAttributes();
		playerinfoText[0].text = "" + plr.GetPower();
		playerinfoText[1].text = "" + plr.GetControl();
		playerinfoText[2].text = "" + plr.GetImpact();
		playerinfoText[3].text = "" + plr.GetSpin();
        
        // ToggleText update
        camToggleText.text = Char.ToUpper(gameRef.GetTarget().ToString()[0]) + gameRef.GetTarget().ToString().ToLower().Substring(1);
        normalToggleText.text = gc.greenNormalMap ? "Angles" : "Normal";
    }
	
	public string floatToPct(string str)
	{
		if (str.Substring(0,1).Equals("0")) //Pct below 100
			return str.Substring(2,Math.Min(2,str.Length - 2));
		if (str.Length == 1) //Whole nums
			return str + "00";
		else if (str.Substring(1,1).Equals(".")) //Whole Num with decimal portion
			return str.Substring(0,1)+""+str.Substring(2);	
		return "ER";
	}
}
