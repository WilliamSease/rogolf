using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Xml;
using UnityEngine.Networking;
using System.IO;
using System;

public class NetworkStaticRead : MonoBehaviour
{
    public NetworkStaticRead nsr;
    private Dictionary<String,XDocument> lookUp = new Dictionary<String,XDocument>();
    
    void Start()
    {
        //if (Application.platform != RuntimePlatform.WebGLPlayer) return;
        nsr.StartCoroutine(nsr.Cache(Application.streamingAssetsPath + "/rogolf_holes.xml"));
        nsr.StartCoroutine(nsr.Cache(Application.streamingAssetsPath + "/range_holes.xml")); 
        nsr.StartCoroutine(nsr.Cache(Application.streamingAssetsPath + "/gooditems.xml")); 
        nsr.StartCoroutine(nsr.Cache(Application.streamingAssetsPath + "/baditems.xml")); 
        nsr.StartCoroutine(nsr.Cache(Application.streamingAssetsPath + "/leaderboard.xml"));
    }
    
    public IEnumerator Cache(string path) {
        UnityWebRequest www = UnityWebRequest.Get(path);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) { throw new Exception(String.Format("Networking FAILED: ({0})", www.error)); }
        else { UnityEngine.Debug.Log(path); lookUp.Add(path, XDocument.Parse(www.downloadHandler.text)); }
    }
    
    public XDocument Get(string path)
    {
        XDocument o;
        lookUp.TryGetValue(path, out o);
        return o;
    }
}
