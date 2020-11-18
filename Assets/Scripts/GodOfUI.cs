using MaterialTypeEnum;
using System;
using System.Collections;
using System.Collections.Generic;
using TargetEnum;
using UnityEngine;
using UnityEngine.UI;
using ShotModeEnum;

public class GodOfUI : MonoBehaviour
{
    public const string NAME = "UICanvas";

    public Game gameRef;
    public GameController gc;

    // Powerbar elements
    public GameObject powerbarParent;
    public Image fillBar;
    public Image negBar;
    public Image barBackground;
    public Slider powerMarker;
    public Slider accMarker;
    public Slider accMarkerNeg;
    private float current;
    private float power;
    private float accuracy;
    private Color barColor;
    public Text[] underText = new Text[4];
    public bool renderPowerbar = true;
    public RawImage powerBarIcon;
    public Texture powerSprite;
    public Texture normalSprite;
    public Texture approachSprite;
    public Text powerShotsRemaining;
    
    //Distance Display Elements
    public GameObject distanceDisplay;
    public Text distanceText;

    // Golfbag elements
    public Text clubText;
    public Text remYdText;
    public Text clubMaxText;

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
	//private float jitterOffset;
    public Text windText;
    public Camera windCamera;
    
    //LieInfo elements
    public Text lieText;
    public Text aAngle;
    public Text bAngle;
    public RawImage lieImage;
    public Texture lieImageGreen;
    public Texture lieImageFairway;
    public Texture lieImageRough;
    public Texture lieImageBunker;
    public Texture lieImageWater;
    public Texture lieImageNone;

    // Bonusinfo elements
    public Text bonusText;
    
    // Playerinfo elements
    public Text[] playerinfoText = new Text[4];
    
    // CamToggleText
    public Text camToggleText;
    public Text normalToggleText;
    
    //HoleWin elements
    public GameObject holeWinDisplay;
    public Text holeWinText;
    public Text holeWinPayout;
    
    //ShotBonus elements
    public GameObject shotBonusDisplay;
    public Text shotBonusText;
    private bool shotBonusActive;
    private float shotBonusTarget;
    private Vector3 shotBonusDefaultPosition;
    
    void Start()
    {
        arrowTarget.transform.parent = arrowParent.transform;
        holeWinDisplay.SetActive(false);
        shotBonusDefaultPosition = shotBonusDisplay.transform.position;
        shotBonusDisplay.SetActive(false);
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

        // ShotMode
        Mode m = gameRef.GetShotMode().GetShotMode();
        switch (m)
        {
            case Mode.NORMAL:
                barColor = Color.yellow;
                powerBarIcon.texture = normalSprite;
                powerShotsRemaining.color = Color.gray;
                powerShotsRemaining.text = gameRef.GetShotMode().GetPowerShots().ToString();
                break;
            case Mode.POWER:
                barColor = Color.red;
                powerBarIcon.texture = powerSprite;
                powerShotsRemaining.color = Color.white;
                powerShotsRemaining.text = gameRef.GetShotMode().GetPowerShots().ToString();
                break;
            case Mode.APPROACH:
                barColor = Color.green;
                powerBarIcon.texture = approachSprite;
                powerShotsRemaining.text = "A";
                break;
            default:
                throw new Exception(String.Format("Unsupported shot mode {0}", m));
        }
        fillBar.color = barColor;
        negBar.color = barColor;
        barBackground.color =  Color.Lerp(barColor, Color.black, .5f);

        // Powerbar update. Do this every frame. Rest can be done whenever
        current = (float) gameRef.GetPowerbar().GetCurrent();
        power = (float) gameRef.GetPowerbar().GetPower();
        accuracy = (float) gameRef.GetPowerbar().GetAccuracy();
        fillBar.fillAmount = current;
        negBar.fillAmount = (current >= -Powerbar.NEGATIVE_PART) ? -current : Powerbar.NEGATIVE_PART;
        powerMarker.value = (power == 0) ? current : power;
        if (current >= 0) {  accMarker.gameObject.SetActive(true); accMarkerNeg.gameObject.SetActive(false); }
        else              {  accMarker.gameObject.SetActive(false); accMarkerNeg.gameObject.SetActive(true); }
        accMarker.value = (accuracy == 0) ? current : accMarker.value;
        accMarkerNeg.value = (accuracy == 0) ? -current : accMarkerNeg.value;
        
        
        int maxVal = (int) MathUtil.ToYardsRounded(gameRef.GetBag().GetClub().GetDistance());
        underText[0].text = (int)((float)maxVal) + "y";
        underText[1].text = (int)((float)maxVal * .75f) + "y";
        underText[2].text = (int)((float)maxVal * .5f) + "y";
        underText[3].text = (int)((float)maxVal * .25f) + "y";

        // Bag update
        clubText.text = gameRef.GetBag().GetClub().GetName();
        remYdText.text = "Pin: " + (int)gameRef.GetBall().DistanceToHole() + "y";
        clubMaxText.text =  maxVal + "y";
        // Hole info update
        HoleInfo holeInfo = gameRef.GetHoleInfo();
        HoleData holeData = gameRef.GetHoleBag().GetCurrentHoleData();
        holeText.text = gameRef.GetHoleBag().GetCurrentHoleNumber().ToString();
        parText.text = "Par " + holeInfo.GetPar().ToString();
        yardText.text = MathUtil.ToYardsRounded(holeInfo.GetLength()).ToString() + "y";
        strokeText.text = holeData != null ? holeData.GetStrokes().ToString() : "";

        // Wind info update
        Wind tWind = gameRef.GetWind();
        Vector3 camAngles = gameRef.GetCameraObject().transform.rotation.eulerAngles;
        // Orient arrow correctly
        camAngles[1] = -camAngles[1] - MathUtil.RadsToDeg((float) tWind.GetAngle()) + 90;
        //Random jittering according to speed. second value is a multiplier
		camAngles[1] += UnityEngine.Random.Range(-tWind.GetSpeed(), tWind.GetSpeed()) * 5;
        camAngles[0] = 0;
        arrowParent.transform.eulerAngles = camAngles; 
        windText.text = String.Format("{0}m", tWind.GetVisualSpeed().ToString("F0"));

        // Lie image update
        if (gameRef.GetState() is PrepareState)
        {
            switch (gameRef.GetBall().GetMaterialType())
            {
                case MaterialType.GREEN:
                    lieImage.texture = lieImageGreen;
                    break;
                case MaterialType.FAIRWAY:
                    lieImage.texture = lieImageFairway;
                    break;
                case MaterialType.ROUGH:
                    lieImage.texture = lieImageRough;
                    break;
                case MaterialType.BUNKER:
                    lieImage.texture = lieImageBunker;
                    break;
                case MaterialType.WATER:
                    lieImage.texture = lieImageWater;
                    break;
                case MaterialType.NONE:
                    lieImage.texture = lieImageNone;
                    break;
                default:
                    throw new Exception(String.Format("Unsupported MaterialType: {0}", gameRef.GetBall().GetMaterialType()));
            } 
        }
        
        // Lie percentage update
        if (gameRef.GetState() is RunningState)
        {
            float lie = gameRef.GetBall().GetLie();
            lieText.text = String.Format("{0}%", (lie * 100).ToString("F0"));
        }
        else
        {
            Tuple<float, float> lieBounds = gameRef.GetBall().GetTerrainType().GetBounds();
            lieText.text = (lieBounds.Item1 == lieBounds.Item2) ? 
                    String.Format("{0}%", (lieBounds.Item1 * 100).ToString("F0")) : 
                    String.Format("{0}%-{1}%", (lieBounds.Item1 * 100).ToString("F0"), (lieBounds.Item2 * 100).ToString("F0"));
        }

        // Lie angle update 
        Tuple<float, float> terrainAngle = gameRef.GetBall().GetTerrainAngle();
        aAngle.text = terrainAngle.Item1.ToString("F0")+"°";
        bAngle.text = terrainAngle.Item2.ToString("F0")+"°";

        // BonusText update
        List<Item> heldItems = gameRef.GetPlayerAttributes().GetHeldItems();
        bonusText.text = "";
        foreach (Item i in heldItems)
        {
            bonusText.text = bonusText.text + i.GetName() + "\n";
        }
        
        // Player attributes update
        PlayerAttributes plr = gameRef.GetPlayerAttributes();
        playerinfoText[0].text = (plr.GetPower() * 100).ToString("F0");
        playerinfoText[1].text = (plr.GetControl() * 100).ToString("F0");
        playerinfoText[2].text = (plr.GetImpact() * 100).ToString("F0");
        playerinfoText[3].text = (plr.GetSpin() * 100).ToString("F0");
        
        // ToggleText update
        camToggleText.text = Char.ToUpper(gameRef.GetTarget().ToString()[0]) + gameRef.GetTarget().ToString().ToLower().Substring(1);
        Target target = gameRef.GetTarget();
        if (target == Target.BALL || target == Target.FREE) camToggleText.text = "View Cursor";
        else camToggleText.text = "Reset Camera";
        normalToggleText.text = gc.greenNormalMap ? "Reset Green" : "View Green Normals";
        
        // Post hole text
        holeWinText.text = MathUtil.GolfTerms(holeData.GetStrokes(), holeInfo.GetPar());
        
        // Shot Bonus update
        if (shotBonusActive)
        {
            if (Time.time >= shotBonusTarget)
            {
                shotBonusActive = false;
                shotBonusDisplay.transform.position =  shotBonusDefaultPosition;
                shotBonusDisplay.SetActive(false);
            }
            else
                shotBonusDisplay.transform.position += (Vector3.up) * Time.deltaTime * 5;
        }
    }
    
    public void ShowHoleResult(int payout) 
    { 
        holeWinDisplay.SetActive(true); 
        holeWinPayout.text = "+" + payout;
    }
    
    public void HideHoleResult() { holeWinDisplay.SetActive(false); }
    
    public void WriteBonus(string s)
    {
        shotBonusText.text = s;
        shotBonusActive = true;
    }
    
    public void InvokeBonus(float to)
    {
        if (shotBonusText.text.Equals("")) return;
        shotBonusTarget = Time.time + to;
        shotBonusDisplay.SetActive(true);
    }
}
