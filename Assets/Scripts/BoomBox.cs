using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour
{
    // Start is called before the first frame update
	private Dictionary<string,AudioSource> lookUp;
	private AudioSource[] sources;
    void Start()
    {
        sources = Component.FindObjectsOfType<AudioSource>();
		lookUp = new Dictionary<string,AudioSource>();
		foreach (AudioSource i in sources)
			lookUp[i.name.ToLower()] = i;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public bool GCPlay(string str)
	{
		if(!lookUp.ContainsKey(str.ToLower())) return false;
		lookUp[str.ToLower()].Play();
		return true;
	}
	
	//Static/Singleton wrapper.
	public static bool Play(string str)
	{
		return GameObject.Find("BoomBox").GetComponent<BoomBox>().GCPlay(str);
	}
}
