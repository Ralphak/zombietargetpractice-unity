//This script is based on code provided by Technicat and Fugu Games
//http://wiki.unity3d.com/index.php?title=PauseMenu

//Menu de pausa. Congela o jogo zerando o timeScale até que o jogador saia da tela.

using UnityEngine;
using System.Collections;
 
public class PauseMenu : MonoBehaviour
{
 
	public GUISkin skin;
 	public GUISkin levelSkin;
	
	private long tris = 0;
	private long verts = 0;
	private float savedTimeScale;
	string currentScene="Main";
	
	bool chickening;
	string resumeText;
	string settingsText;
	string quitText;
	string warningText;
	string yes;
	string no;
	string sensiText;
	string musicText;
	string languageText;
	string backText;
	string musicState;
 
	void Start() {
	    Time.timeScale = 1;
		GetComponent<AudioSource>().ignoreListenerPause=true;
	}
 
	void LateUpdate () {
		if (Input.GetKeyDown("escape") && Score.zombieCounter>0) 
		{
			if(Time.timeScale==0)
			{
				UnPauseGame();
				currentScene="Main";
			}
			else PauseGame(); 
		}
		if(chickening && !GetComponent<AudioSource>().isPlaying) 
		{
			Application.LoadLevel("menu");
			Score.Reset();
			UnPauseGame();
		}
		
		if(Menu.Language=="Portugues")
		{
			resumeText="Continuar";
			settingsText="Opcoes";
			quitText="Sair";
			warningText="Tem certeza de que vai arregar?";
			yes="Sim";
			no="Nao";
			sensiText="Sensibilidade";
			musicText="Musica";
			languageText="Idioma";
			backText="Voltar";
		}
		else
		{
			resumeText="Resume";
			settingsText="Settings";
			quitText="Quit";
			warningText="Do you really want to chicken out?";
			yes="Yes";
			no="No";
			sensiText="Look Sensitivity";
			musicText="Music";
			languageText="Language";
			backText="Back";
		}
	}
 
	void OnGUI () 
	{		
	    if (skin != null) {
	        GUI.skin = skin;
	    }
	    if (IsGamePaused()) {
	        MainPauseMenu();
	    }
	}  
 
 	//Aqui é definido o menu de pausa.
	void MainPauseMenu() 
	{
	    GUI.Box(new Rect (0, 0, Screen.width, Screen.height),"");
		
		switch(currentScene)
		{
		case "Main":
			if(GUI.Button(new Rect(Screen.width/2-120,Screen.height/2-75,240,skin.button.fontSize),resumeText))
			{
				UnPauseGame();
			}
			if(GUI.Button(new Rect(Screen.width/2-110,Screen.height/2,220,skin.button.fontSize),settingsText))
			{
				currentScene="Settings";
			}
			if(GUI.Button(new Rect(Screen.width/2-60,Screen.height/2+75,120,skin.button.fontSize),quitText))
			{
				currentScene="ConfirmQuit";
			}
			break;
			
		case "Settings":
			//Ajuste de sensibilidade do mouse
			GUI.Label(new Rect(Screen.width/2-300, 100, 300, skin.customStyles[0].fontSize),
				sensiText,skin.customStyles[0]);
			Weapon.sensitivityMultiplier = GUI.HorizontalSlider(new Rect(Screen.width/2, 110, 200, 20),
				Weapon.sensitivityMultiplier,1,5);
			
			//Liga e desliga a música do jogo. Accesa o script MusicToggle.
			GUI.Label(new Rect(Screen.width/2-140, 150, 100, skin.customStyles[0].fontSize),
				musicText,skin.customStyles[0]);
			if(GUI.Button(new Rect(Screen.width/2, 153, 70, levelSkin.button.fontSize*1.2f),
				musicState, levelSkin.button))
				MusicToggle.toggleMusic=!MusicToggle.toggleMusic;
			
			if(MusicToggle.toggleMusic) musicState="ON";
			else musicState="OFF";
			
			//Muda o idioma do jogo
			GUI.Label(new Rect(Screen.width/2-200, 200, 200, skin.customStyles[0].fontSize),
				languageText,skin.customStyles[0]);
			if(GUI.Button(new Rect(Screen.width/2, 203, 150, levelSkin.button.fontSize*1.2f),
				Menu.Language, levelSkin.button))
			{
				if(Menu.Language=="English") Menu.Language="Portugues";
				else if (Menu.Language=="Portugues") Menu.Language="English";
			}
			
			//Volta ao principal
			if(GUI.Button(new Rect(Screen.width/2-80,Screen.height*0.8f,160,skin.button.fontSize),backText))
			{
				currentScene="Main";
			}
			
			break;
			
		case "ConfirmQuit":
			if(!chickening)
			{
				GUI.BeginGroup (new Rect (0, 0, Screen.width, Screen.height),warningText,skin.box);
				
				if(GUI.Button (new Rect(
					Screen.width/2-120, Screen.height/2+skin.box.fontSize/2, 110, 50), no))
				{ 
					currentScene="Main";
				}
				
				if(GUI.Button (new Rect(
					Screen.width/2+20, Screen.height/2+skin.box.fontSize/2, 110, 50), yes))
				{ 
					GetComponent<AudioSource>().Play();
					chickening=true;
				}
		
				GUI.EndGroup ();
			}
			break;
		}
	}
 
	void GetObjectStats() {
	    verts = 0;
	    tris = 0;
	    GameObject[] ob = FindObjectsOfType(typeof(GameObject)) as GameObject[];
	    foreach (GameObject obj in ob) {
	        GetObjectStats(obj);
	    }
	}
 
	void GetObjectStats(GameObject obj) {
	   	Component[] filters;
	    filters = obj.GetComponentsInChildren<MeshFilter>();
	    foreach( MeshFilter f  in filters )
	    {
	        tris += f.sharedMesh.triangles.Length/3;
	      	verts += f.sharedMesh.vertexCount;
	    }
	}
 
	void PauseGame() {
	    savedTimeScale = Time.timeScale;
	    Time.timeScale = 0;
	    AudioListener.pause = true;
	}
 
	void UnPauseGame() {
	    Time.timeScale = savedTimeScale;
	    AudioListener.pause = false; 
	}
 
	bool IsGamePaused() {
	    return (Time.timeScale == 0);
	}
 
	void OnApplicationPause(bool pause) {
	    if (IsGamePaused()) {
	        AudioListener.pause = true;
	    }
	}
}