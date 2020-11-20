using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using System.Xml;
using UnityEngine.Networking;

public class NetworkingUtil
{
    public static XDocument NetworkLoad(string path)
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            return XDocument.Load(path);
        //This code is executed for WEBGL.
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        return XDocument.Parse(uwr.downloadHandler.text);
    }
    
    public static XmlWriter NetworkWrite(string path)
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            return XmlWriter.Create(path);
        //This code is executed for WEBGL.
        UnityWebRequest uwr = UnityWebRequest.Get(path);
        return XmlWriter.Create(uwr.downloadHandler.text); //This dosen't really make sense. It compiles but it can't be right
    }
}
