using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{
    private GameObject godObject;

    public Text[] Hole = new Text[18];
    public Text[] Back = new Text[18];
    public Text[] Front = new Text[18];
    public Text[] Par = new Text[18];
    public Text[] Hcp = new Text[18];
    public Text[] Strokes = new Text[18];
    public Text[] Putts = new Text[18];
    public Text[] Fir = new Text[18];
    public Text[] Gir = new Text[18];
    public Text[] tot = new Text[18];
    
    void Start()
    {
        godObject = GodObject.Create();
        godObject.AddComponent<Game>();
        Game game = godObject.GetComponent<Game>();
        game.CreateGameData();

        for(int i = 0; i < 18; i++)
        {
            Hole[i].text = (i+1).ToString();
            Back[i].text = "";
            Front[i].text = "";
            Par[i].text = "";
            Par[i].fontStyle = FontStyle.Bold;
            Hcp[i].text = "";
            Strokes[i].text = "";
            Putts[i].text = "";
            Fir[i].text = "";
            Gir[i].text = "";
        }

        tot[0].text = "OUT";
        tot[1].text = "SumBack";
        tot[2].text = "SumFront";
        tot[3].text = "SumPar";
        tot[4].text = "AvgHcp";
        tot[5].text = "SumStrokes";
        tot[6].text = "SumPutts";
        tot[7].text = "SumFir";
        tot[8].text = "SumGir";

        tot[9].text = "IN";
        tot[10].text = "SumBack";
        tot[11].text = "SumFront";
        tot[12].text = "SumPar";
        tot[13].text = "AvgHcp";
        tot[14].text = "SumStrokes";
        tot[15].text = "SumPutts";
        tot[16].text = "SumFir";
        tot[17].text = "SumGir";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Need not save game data
            UnityEngine.Object.Destroy(godObject);

            // Load item scene
            SceneManager.LoadScene("ItemScene");
        }
    }
}
