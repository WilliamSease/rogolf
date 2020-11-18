using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
To add a sound to the game:
1) Create new AudioSource that is child of BoomBox
2) Append .ogg or .Mp3 in inspector
3) Add new enum which is the uppercase name of that AudioSource game object
4) This class does the rest.
Calling Syntax : Play(SoundEnum.Sound.[SomeSound]);
**/
namespace SoundEnum
{
    public enum Sound { TEST, APPLAUSE, PURCHASE, CLICK, PUTTHIT, IRONHIT, WEDGEHIT, SINK, SPLASH };
}

public class BoomBox : MonoBehaviour
{
	public float globalVolume = 1.0f;
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
	
	public bool GCPlay(string str, float vol = 1.0f)
	{
		if(!lookUp.ContainsKey(str.ToLower())) return false;
		AudioSource s = lookUp[str.ToLower()];
		s.PlayOneShot(s.clip, globalVolume * vol);
		return true;
	}
	
	//Singleton wrapper.
	public static bool Play(SoundEnum.Sound b) { return GameObject.Find("BoomBox").GetComponent<BoomBox>().GCPlay(b.ToString()); }
	public static bool Play(SoundEnum.Sound b, float vol) { return GameObject.Find("BoomBox").GetComponent<BoomBox>().GCPlay(b.ToString(), vol); } //This one lets you * a float by globalVolume to create a smaller sound
	
	public static void SetVolumeStat(float vol) {GameObject.Find("BoomBox").GetComponent<BoomBox>().SetVolume(vol);}
	public void SetVolume(float vol) {if (vol >= 0.0f && vol <= 1.0f) globalVolume = vol;}
	public static float GetVolumeStat() {return GameObject.Find("BoomBox").GetComponent<BoomBox>().GetVolume();}
	public float GetVolume() {return globalVolume;}
}
