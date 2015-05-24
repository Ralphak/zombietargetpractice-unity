//Tela de resultados. Aqui aparece as estatísticas da última partida.

using UnityEngine;
using System.Collections;

public class Results : MonoBehaviour {

	public GUISkin skin;
	int spacing=40;
	float accuracy;
	int timeTaken;
	string resultsText;
	string timeText;
	string secText;
	string accText;
	string headshotText;
	string peopleText;
	string outofText;
	string menuText;
	
	
	//Ajusta os dados e (se for o caso) envia ao Kongregate
	void Start()
	{
		Screen.lockCursor=false;
		Score.timeTaken=Score.timeTaken*1000;
		timeTaken=(int)Score.timeTaken;
		accuracy = (Score.zombiesOnMap/Score.shotsFired)*100;
		UploadtoKongregate();
		
		if(Menu.Language=="Portugues")
		{
			resultsText="RESULTADOS";
			timeText="Tempo gasto: ";
			secText=" segundos";
			accText="Precisao: ";
			headshotText="Tiros na cabeca: ";
			peopleText="Pessoas vivas: ";
			outofText=" de ";
			menuText="Voltar para o Menu";
		}
		else
		{
			resultsText="RESULTS";
			timeText="Time taken: ";
			secText=" seconds";
			accText="Accuracy: ";
			headshotText="Headshots: ";
			peopleText="People alive: ";
			outofText=" out of ";
			menuText="Return to Menu";
		}
	}
	
	void OnGUI()
	{		
		GUI.skin = skin;
		
		GUI.Label(new Rect(0,10,Screen.width,skin.customStyles[0].fontSize), resultsText,skin.customStyles[0]);
			
		GUI.Label(new Rect(0, 100, Screen.width, skin.customStyles[1].fontSize),Score.mapName,skin.customStyles[1]);
			
		GUI.Label(new Rect(0, 140, Screen.width, skin.label.fontSize+10), 
			timeText+timeTaken/1000+secText);
		GUI.Label(new Rect(0, 140+spacing, Screen.width, skin.label.fontSize+10), 
			accText+(int)accuracy+"%");
		GUI.Label(new Rect(0, 140+spacing*2, Screen.width, skin.label.fontSize+10), 
			headshotText+Score.headshots+outofText+Score.zombiesOnMap);
		GUI.Label(new Rect(0, 140+spacing*3, Screen.width, skin.label.fontSize+10), 
			peopleText+Score.peopleAlive+outofText+Score.peopleOnMap);
		
		if(GUI.Button(new Rect(Screen.width/2-125,Screen.height*0.8f,250,skin.button.fontSize*1.5f),menuText))
		{
			Application.LoadLevel("menu");
			Score.Reset();
		}	
	}
	
	void UploadtoKongregate()
	{
		switch(Score.mapName)
		{
		case "Green Building":
			KongregateAPI.SubmitStatistic("Time Taken (Green Building)",timeTaken);
			KongregateAPI.SubmitStatistic("Accuracy (Green Building)",(int)accuracy);
			KongregateAPI.SubmitStatistic("Headshots (Green Building)",Score.headshots);
			KongregateAPI.SubmitStatistic("People Alive (Green Building)",Score.peopleAlive);
			break;
		case "Wasteland":
			KongregateAPI.SubmitStatistic("Time Taken (Wasteland)",timeTaken);
			KongregateAPI.SubmitStatistic("Accuracy (Wasteland)",(int)accuracy);
			KongregateAPI.SubmitStatistic("Headshots (Wasteland)",Score.headshots);
			break;	
		}
	}
}
