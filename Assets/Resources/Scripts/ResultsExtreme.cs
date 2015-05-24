//Tela de resultados. Aqui aparece as estatísticas da última partida.

using UnityEngine;
using System.Collections;

public class ResultsExtreme : MonoBehaviour {

	public GUISkin skin;
	int spacing=40;
	float accuracy;
	
	string resultsText;
	string waveText;
	string accText;
	string headshotText;
	string menuText;
	
	
	//Ajusta os dados e (se for o caso) envia ao Kongregate
	void Start()
	{
		Screen.lockCursor=false;
		if(Score.shotsFired!=0) accuracy = (Score.zombiesHit/Score.shotsFired)*100;
		UploadtoKongregate();
		
		if(Menu.Language=="Portugues")
		{
			resultsText="RESULTADOS";
			waveText="Ondas completas: ";
			accText="Precisao: ";
			headshotText="Tiros na cabeca: ";
			menuText="Voltar para o Menu";
		}
		else
		{
			resultsText="RESULTS";
			waveText="Waves completed: ";
			accText="Accuracy: ";
			headshotText="Headshots: ";
			menuText="Return to Menu";
		}
	}
	
	void OnGUI()
	{		
		GUI.skin = skin;
		
		GUI.Label(new Rect(0,10,Screen.width,skin.customStyles[0].fontSize), resultsText,skin.customStyles[0]);
			
		GUI.Label(new Rect(0, 100, Screen.width, skin.customStyles[1].fontSize),Score.mapName,skin.customStyles[1]);
			
		GUI.Label(new Rect(0, 140, Screen.width, skin.label.fontSize+10), 
			waveText+(Score.currentWave-1));
		GUI.Label(new Rect(0, 140+spacing, Screen.width, skin.label.fontSize+10), 
			accText+(int)accuracy+"%");
		GUI.Label(new Rect(0, 140+spacing*2, Screen.width, skin.label.fontSize+10), 
			headshotText+Score.headshots);
		
		if(GUI.Button(new Rect(Screen.width/2-125,Screen.height*0.8f,250,skin.button.fontSize*1.5f),menuText))
		{
			Application.LoadLevel("menu");
			Score.Reset();
		}	
	}
	
	void UploadtoKongregate()
	{
		KongregateAPI.SubmitStatistic("Waves Completed (Extreme)",Score.currentWave-1);
		KongregateAPI.SubmitStatistic("Accuracy (Extreme)",(int)accuracy);
		KongregateAPI.SubmitStatistic("Headshots (Extreme)",Score.headshots);
	}
}
