using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LeaderBoardController : MonoBehaviour
{
    public const string LEADERBOARD = "leaderboard.xml";
    public const string LEADERBOARD_BACKUP = "leaderboard_backup.xml";
    public const string SCENE_NAME = "ResultsScene";
    
    private int score;
    private int stroke;
    public Text scoreText;
    
    public Text[] names = new Text[5];
    public Text[] dates = new Text[5];
    public Text[] strokes = new Text[5];
    public Text[] scores = new Text[5];
    
    private bool submitted = false;
    public Button submitButton;
    public InputField inputField;
    
    // Start is called before the first frame update
    void Start()
    {
            Game game = GameObject.Find(GameController.NAME).GetComponent<Game>();
            score = game.GetScore().GetEarnings();
            stroke = game.GetHoleBag().GetHolesPlayed().Sum(h => h.GetStrokes());
            scoreText.text = "" + score; //TODO get this from GAME
            DisplayAllRecords();
            submitButton.GetComponent<Button>().onClick.AddListener(submit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void submit()
    {
        if (submitted) return;
        //TODO, get these dummy values from GAME.
        submitted = appendSubmission(inputField.text, System.DateTime.Now.ToString("MM/dd/yyyy"), stroke, score);
    }
    
    public void DisplayRecord(int pos, Record r)
    {
        names[pos].text = r.name;
        dates[pos].text = r.date;
        strokes[pos].text = r.stroke;
        scores[pos].text = r.score;
    }
    
    public void DisplayAllRecords()
    {
            List<Record> toDisplay = ReadXMLFromDisk();
            toDisplay = SortByScores(toDisplay);
            int i = 0;
            foreach (Record rec in toDisplay)
            {
                DisplayRecord(i, rec);
                i++;
            }
    }
    
    public bool appendSubmission(string name, string date, int theStroke, int theScore)
    {
        if (name.Length < 1) return false;
        string stroke = "" + theStroke;
        string score = "" + theScore;
        Record newRecord = new Record (name, date, stroke, score);
        List<Record> current = ReadXMLFromDisk();
        current.Add(newRecord);
        current = SortByScores(current);
        XmlWriter xmlWriter = NetworkingUtil.NetworkWrite(Application.streamingAssetsPath + "/" + LEADERBOARD);
        
        xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("records");
            foreach (Record r in current)
            {
                xmlWriter.WriteStartElement("record");
                
                    xmlWriter.WriteStartElement("name");
                    xmlWriter.WriteString(r.name);
                    xmlWriter.WriteEndElement();
                    
                    xmlWriter.WriteStartElement("date");
                    xmlWriter.WriteString(r.date);
                    xmlWriter.WriteEndElement();
                    
                    xmlWriter.WriteStartElement("stroke");
                    xmlWriter.WriteString(r.stroke);
                    xmlWriter.WriteEndElement();
                    
                    xmlWriter.WriteStartElement("score");
                    xmlWriter.WriteString(r.score);
                    xmlWriter.WriteEndElement();
                    
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
        xmlWriter.WriteEndDocument();
        xmlWriter.Close();
        DisplayAllRecords();
        return true;
    }
    
    public List<Record> ReadXMLFromDisk()
    {
        try
        {
            XDocument xml = NetworkingUtil.NetworkLoad(Application.streamingAssetsPath + "/" + LEADERBOARD);
            return (from Record in xml.Root.Elements("record")
                        select new Record()
                        {
                            name = (string) Record.Element("name"),
                            date = (string) Record.Element("date"),
                            stroke = (string) Record.Element("stroke"),
                            score = (string) Record.Element("score"),
                        }).ToList();
        }
        catch
        {
            UnityEngine.Debug.Log("leaderboard parse error!");
            throw new Exception(string.Format("leaderboard parse error ({0})", LEADERBOARD));
        }

    }
    
    public List<Record> SortByScores(List<Record> toProcess)
    {
        List<Record> ret = new List<Record>(5); //Only get the 5 best scores.
        int localMax;
        Record tempRec;
        for (int i = 0; i < 5; i++)
        {
            tempRec = new Record();
            localMax = 0;
            foreach (Record rec in toProcess)
            {
                    int tScore = int.Parse(rec.score);
                    if (tScore > localMax)
                    {
                        tempRec = rec;
                        localMax = tScore;
                    }
            }
                ret.Add(tempRec);
                toProcess.Remove(tempRec);
        }
        return ret;
    }
    
    public static bool WriteDummies()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer) //Can't have people resetting the board :^>
            return false;
        string sourceFile = Application.streamingAssetsPath + "/"  + LEADERBOARD_BACKUP;  
        string destinationFile = Application.streamingAssetsPath + "/"  + LEADERBOARD;
        try { File.Copy(sourceFile, destinationFile, true); }
        catch (IOException iox) { Console.WriteLine(iox.Message); return false;}
        return true;
    }
}

public class Record
{
    public Record (string name, string date, string stroke, string score) {this.name = name; this.date = date; this.stroke = stroke; this.score = score;}
    public Record() {}
    public string name { get; set; }
    public string date { get; set; }
    public string stroke { get; set; }
    public string score { get; set; }
}
