using UnityEngine;
using System.Collections;

public class BackWarning : MonoBehaviour {
	
	public static int zombiesInTrigger;
	public Texture2D warningSignal;
	bool showWarning;
	
	void Update()
	{
		if(zombiesInTrigger>0) showWarning=true;
		else showWarning=false;
	}
	
	void OnGUI()
	{
		//Warning signal
		if(showWarning) GUI.DrawTexture(new Rect(Screen.width/2-warningSignal.width/2,Screen.height*0.75f,
			warningSignal.width,warningSignal.height),
			warningSignal);
	}
}
