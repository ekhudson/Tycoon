using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioList : Singleton<AudioList> 
{
	
	public AudioClip[] SFX;
	//public AudioClip[] MusicTracks;
	public List<AudioClip> MusicTracks = new List<AudioClip>();
	

}
