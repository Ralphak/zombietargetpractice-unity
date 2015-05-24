//Ingame HUD. Aqui aparece quantos zumbis faltam e o tempo decorrido desde o ínicio da rodada.
//Adaptado para o modo extremo

using UnityEngine;
using System.Collections;

public class HUDExtreme : MonoBehaviour {
	
	public GUISkin skin;
	public GUISkin title;
	public Texture2D zombieIcon;
	string centerText;
	string defeatText;
	bool defeated;
	int levelCountdown;
	int flashTimer;
	public SoundPlayer sound;
	public Texture2D heart;
	public Texture2D emptyHeart;
	Texture2D heart1;
	Texture2D heart2;
	
	void Update()
	{			
		if(defeated)
		{
			if(Menu.Language=="Portugues") defeatText = "DERROTADO!";
			else defeatText = "DEFEATED!";
			levelCountdown++;
			if(levelCountdown>=180) Application.LoadLevel("resultsExtreme");
		}
		else if(Score.zombieCounter==0) 
		{
			if(Menu.Language=="Portugues") centerText = "ONDA "+Score.currentWave;
			else centerText = "WAVE "+Score.currentWave;
			
			sound.Play("go");
		}
		else centerText="";
		
		switch (Score.lifeCounter)
		{
		case 2:
			heart1=heart;
			heart2=heart;
			break;
		case 1:
			heart1=heart;
			heart2=emptyHeart;
			break;
		default:
			defeated=true;
			heart1=emptyHeart;
			heart2=emptyHeart;
			break;
		}
	}
	
	void OnGUI()
	{	
		GUI.skin = skin;
		
		//Zombies Left
		GUI.DrawTexture(new Rect(
			zombieIcon.width/4,
			Screen.height - zombieIcon.height*1.3f,
			zombieIcon.width, zombieIcon.height),
			zombieIcon);
		GUI.Label(new Rect(
			zombieIcon.width/4 + zombieIcon.width,
			Screen.height - zombieIcon.height*1.16f,
			100, skin.label.fontSize),
			Score.zombieCounter.ToString());
		
		//Heart Counter
		GUI.DrawTexture(new Rect(zombieIcon.width+100, Screen.height-50, 32, 32), heart1);
		GUI.DrawTexture(new Rect(zombieIcon.width+132, Screen.height-50, 32, 32), heart2);
		
		// Headshot
		if(Score.headshotFlash) 
		{
			if(Time.timeScale!=0) flashTimer++;
			if(flashTimer>=150)
			{
				flashTimer=0;
				Score.headshotFlash=false;
			}
			GUI.Label(new Rect(
			zombieIcon.width + 250,
			Screen.height - zombieIcon.height*1.16f,
			200, skin.label.fontSize),
			"Headshot!");
		}
		// Penalidade
		else if(Score.wrongFlash) 
		{
			if(Time.timeScale!=0) flashTimer++;
			if(flashTimer>=150)
			{
				flashTimer=0;
				Score.wrongFlash=false;
			}
			GUI.Label(new Rect(
			zombieIcon.width + 250,
			Screen.height - zombieIcon.height*1.16f,
			150, skin.label.fontSize),
			"Alert!", skin.customStyles[2]);
		}
		
		//Center Text
		if(defeated) GUI.Label(new Rect(0, 0, Screen.width, Screen.height*0.9f), defeatText, title.customStyles[0]);
		else GUI.Label(new Rect(0, 0, Screen.width, Screen.height*0.9f), centerText, title.label);
	}
}