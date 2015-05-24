using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {
	
	public AudioClip go;
	public AudioClip headshot;
	public AudioClip wrong;
	
	// Use this for initialization
	void Start () 
	{
		Play("go");
	}
	
	public void Play(string sound)
	{
		switch(sound)
		{
		case "go": GetComponent<AudioSource>().clip=go;
			break;
		case "headshot": GetComponent<AudioSource>().clip=headshot;
			break;
		case "wrong": GetComponent<AudioSource>().clip=wrong;
			break;
		}
		GetComponent<AudioSource>().Play();
	}
}
