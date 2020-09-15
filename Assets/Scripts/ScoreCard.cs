using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 18; i++)
        {
            Hole[i].text = "hol" + i;
            Back[i].text = "back" + i;
            Front[i].text = "frnt" + i;
            Par[i].text = "par" + i;
            Par[i].fontStyle = FontStyle.Bold;
            Hcp[i].text = "hcp" + i;
            Strokes[i].text = "strk" + i;
            Putts[i].text = "ptts" + i;
            Fir[i].text = "fir" + i;
            Gir[i].text = "gir" + i;
            tot[i].text = "tot" + i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
