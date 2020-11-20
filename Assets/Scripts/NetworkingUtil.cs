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
        return XDocument.Load(uwr.downloadHandler.text);
    }
    
    public static XmlWriter NetworkWrite(string path)
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            return XmlWriter.Create(path);
        return null;
    }
}
