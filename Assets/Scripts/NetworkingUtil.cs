using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Xml.Linq;
using System.Xml;
using UnityEngine.Networking;
using System.IO;
using System;

public class NetworkingUtil : MonoBehaviour
{
    public NetworkingUtil nu;
    public Canvas wait;
    public static string toParse;
    
    public void Start()
    {
        wait.enabled = false;
    }
    
    public static XDocument NetworkLoad(string path)
    {
        
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            return XDocument.Load(path);
        return GameObject.Find("GameController").GetComponent<NetworkStaticRead>().Get(path); 
    }
    
    public static XmlWriter NetworkWrite(string path)
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            return XmlWriter.Create(path);
        //This code is executed for WEBGL.
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        return XmlWriter.Create(uwr.downloadHandler.text);
    }
}
