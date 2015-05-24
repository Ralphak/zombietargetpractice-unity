using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {
	
	float alpha;
	float alphaSpeed;
	int fadeTimer;
	bool fading;
	public GUIStyle style;
	public GUISkin skin;
	public Texture2D sevenLogo;
	string currentScene;
	
	void Start()
	{
		KongregateAPI.Initialize();
		currentScene="Language";
		alphaSpeed=0.005f;
	}
	
	// Use this for initialization
	void OnGUI () 
	{
		switch (currentScene)
		{
		#region Intro
		case "Intro":
			GUI.color = new Color(
				style.normal.textColor.r,
				style.normal.textColor.g,
				style.normal.textColor.b,
				alpha);
			
			if(Menu.Language=="Portugues") GUI.Label(new Rect(0,0,Screen.width,Screen.height),
				"Um jogo desenvolvido por\nRafael Alves", style);
			else GUI.Label(new Rect(0,0,Screen.width,Screen.height),
				"A game developed by\nRafael Alves", style);
			
			if(fading)
			{
				if(alpha<=0) Application.LoadLevel("menu");
				else alpha -= alphaSpeed;
			}
			else
			{
				if(alpha>=1)
				{
					fadeTimer++;
					if(fadeTimer>=100) fading=true;
				} 
				else alpha+=alphaSpeed;
			}
			break;
		#endregion
		
		#region Language
		case "Language":
			if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2-75,200,skin.button.fontSize),"English",skin.button))
			{
				currentScene="Intro";
			}
			if(GUI.Button(new Rect(Screen.width/2-125,Screen.height/2+25,250,skin.button.fontSize),"Portugues",skin.button))
			{
				Menu.Language="Portugues";
				currentScene="Intro";
			}
			break;
		#endregion
		}
	}
}
