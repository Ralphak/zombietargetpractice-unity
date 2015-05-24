using UnityEngine;
using System.Collections;

public class MusicToggle : MonoBehaviour 
{
	public static bool toggleMusic=true;
	bool playing;
	
	// Liga e desliga a música baseado na escolha do jogador.
	void Update () 
	{
		if(toggleMusic && !playing) 
		{
			GetComponent<AudioSource>().Play();
			playing=true;
		}
		else if(!toggleMusic)
		{
			GetComponent<AudioSource>().Stop();
			playing=false;
		}
	}
}
