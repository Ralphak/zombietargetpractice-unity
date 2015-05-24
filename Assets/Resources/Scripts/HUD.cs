//Ingame HUD. Aqui aparece quantos zumbis faltam e o tempo decorrido desde o ínicio da rodada.

using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	
	public GUISkin skin;
	public GUISkin title;
	public Texture2D zombieIcon;
	public Texture2D timerIcon;
	float timer;
	string centerText = "GO!";
	bool clearGo;
	bool endGo;
	int levelCountdown;
	public static bool headshotFlash;
	public static bool wrongFlash;
	int flashTimer;
	public SoundPlayer sound;
	
	void Update()
	{	
		//Retira o GO! que aparece no começo da partida.
		if(timer >=1 && !clearGo)
		{
			centerText = "";
			clearGo=true;
		}
		
		//Para o contador quando o jogo termina e abre a tela de resultados.
		if(endGo)
		{
			levelCountdown++;
			if(levelCountdown>=180) Application.LoadLevel("results");
		}
		else
		{
			if(Score.zombieCounter==0) 
			{
				endGo=true;
				if(Menu.Language=="Portugues") centerText = "TODOS OS ZUMBIS CAIRAM!";
				else centerText = "ALL ZOMBIES WERE KILLED!";
				sound.Play("go");
			}
			else timer = Mathf.Round(Time.timeSinceLevelLoad*1000)/1000;
		}		
		
		//Envia o contador de tempo ao score. Inclui bônus e penalidades.
		Score.timeTaken=timer
			-Score.headshots*3
			+(Score.peopleOnMap-Score.peopleAlive)*10;		
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
		
		//Time Elapsed
		GUI.DrawTexture(new Rect(
			zombieIcon.width + timerIcon.width,
			Screen.height - timerIcon.height*0.85f,
			timerIcon.width/2, timerIcon.height/2),
			timerIcon);
		GUI.Label(new Rect(
			zombieIcon.width + timerIcon.width*1.55f,
			Screen.height - zombieIcon.height*1.16f,
			150, skin.label.fontSize),
			Score.timeTaken.ToString());
		if(headshotFlash) // Headshot
		{
			if(Time.timeScale!=0) flashTimer++;
			if(flashTimer>=150)
			{
				flashTimer=0;
				headshotFlash=false;
			}
			GUI.Label(new Rect(
			zombieIcon.width + timerIcon.width*3.5f,
			Screen.height - zombieIcon.height*1.16f,
			210, skin.label.fontSize),
			"-3 Headshot!");
		}
		else if(wrongFlash) // Penalidade
		{
			if(Time.timeScale!=0) flashTimer++;
			if(flashTimer>=150)
			{
				flashTimer=0;
				wrongFlash=false;
			}
			GUI.Label(new Rect(
			zombieIcon.width + timerIcon.width*3.5f,
			Screen.height - zombieIcon.height*1.16f,
			210, skin.label.fontSize),
			"+10 Penalty!", skin.customStyles[2]);
		}
		
		//Center Text
		GUI.Label(new Rect(0, 0, Screen.width, Screen.height*0.9f),
			centerText, title.label);
	}
}