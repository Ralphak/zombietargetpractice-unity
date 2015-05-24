using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public GUISkin skin;
	public GUISkin title;
	public GUISkin levelSkin;
	public Texture2D arrowleft;
	public Texture2D arrowright;
	public Texture2D back;
	
	Texture2D currentPreview;
	public Texture2D preview;
	public Texture2D wasteland;
	public Texture2D wastelandExtreme;
	public Texture2D greenbuilding;
		
	string playerName;
	string levelName;
	string authorName;
	string instruction;
	string currentScene="main";	
	int currentPage=1;
	string musicState;
	
	public static string Language="English"; //Eu sei, é static. Desculpem. :(
	string loadingText;
	string welcomeText;
	string startText;
	string instructionsText;
	string settingsText;
	string sensiText;
	string musicText;
	string languageText;
	string choosemapText;
	string byText;
	string practiceText;
	string extremeText;
	
	void OnGUI()
	{
		GUI.skin = skin;
		
		if(Language=="Portugues")
		{
			loadingText="CARREGANDO, AGUARDE...";
			welcomeText="Bem vindo a bordo, ";
			startText="Jogar";
			instructionsText="Instrucoes";
			settingsText="Opcoes";
			sensiText="Sensibilidade";
			musicText="Musica";
			languageText="Idioma";
			choosemapText="Escolha um Mapa";
			byText="por ";
			practiceText="Modo Pratica";
			extremeText="Modo Extremo";
		}
		else
		{
			loadingText="LOADING, PLEASE WAIT...";
			welcomeText="Welcome on board, ";
			startText="Start";
			instructionsText="Instructions";
			settingsText="Settings";
			sensiText="Look Sensitivity";
			musicText="Music";
			languageText="Language";
			choosemapText="Choose a Map";
			byText="by ";
			practiceText="Practice Mode";
			extremeText="Extreme Mode";
		}
		
		//Nome do jogador no Kongregate, ou Visitante se estiver offline
		if(KongregateAPI.userName=="Guest" && Language=="Portugues") playerName="Visitante";
		else playerName=KongregateAPI.userName;
		
		switch(currentScene)
		{
		case "main":MainMenu();
			break;
		case "instructions":Instructions();
			break;
		case "settings":Settings();
			break;
		case "levelselect":LevelSelect();
			break;
		case "loading": GUI.Label(new Rect(0,0,Screen.width,Screen.height), loadingText, title.label);
			break;
		}
	}
	
	void MainMenu()// Menu Principal
	{
		GUI.Label(new Rect(0, 0, Screen.width, title.label.fontSize), "ZOMBIE TARGET PRACTICE",title.label);
		
		GUI.Label(new Rect(0,Screen.height*0.11f,Screen.width, skin.label.fontSize), welcomeText+playerName, skin.customStyles[0]);
		
		if(GUI.Button(new Rect(Screen.width/2-250,Screen.height/2,150,80), startText)) currentScene="levelselect" ;
		if(GUI.Button(new Rect(Screen.width/2-25,Screen.height/2,300,80), instructionsText)) currentScene="instructions";
		if(GUI.Button(new Rect(Screen.width-220,Screen.height-60,210,50), settingsText)) currentScene="settings";
	}
	
	//Ferramenta para mudar de página e retornar ao menu principal.
	//Se totalPages=1 aparecerá apenas o botão Back.
	void PageTurner(int totalPages=1)
	{
		if(totalPages>1) GUI.Label(new Rect(Screen.width/2-35,Screen.height-75,70,title.label.fontSize),currentPage.ToString(),title.label);		
		if(currentPage>1 && GUI.Button(new Rect(
				Screen.width/2-arrowleft.width-40, Screen.height-arrowleft.height,
				arrowleft.width, arrowleft.height),
				arrowleft, skin.label))
			currentPage--;
		if(currentPage<totalPages && GUI.Button(new Rect(
				Screen.width/2+50, Screen.height-arrowright.height,
				arrowright.width, arrowright.height),
				arrowright, skin.label))
			currentPage++;
		if(GUI.Button(new Rect(Screen.width*0.01f,Screen.height*0.99f-back.height,back.width,back.height), back, skin.label))
		{
			currentPage=1;
			currentScene="main";
		}
	}
	
	void Instructions() //Tela de instruções
	{
		GUI.Box(new Rect(0,0,Screen.width,Screen.height), instructionsText,title.box);
		PageTurner(3);
		GUI.Label(new Rect(Screen.width*0.1f,Screen.height*0.1f,Screen.width*0.8f,Screen.height*0.8f), instruction, skin.customStyles[1]);
				
		switch(currentPage)
		{
		case 1: 
			if(Language=="Portugues") instruction="Mover: WSAD ou Setas \n" +
				"Disparar Arma: Botao esquerdo do mouse \n" +
				"Mirar Arma: Botao direito do mouse (Segure) \n" +
				"Pular (quando nao mirando): Barra de Espaco ou Ctrl Direito";
			
			else instruction="Move Around: WSAD or Arrow Keys \n" +
				"Fire Weapon: Left Mouse Button \n" +
				"Aim Weapon: Right Mouse Button (Hold) \n" +
				"Jump (when not aiming): Spacebar or Right Ctrl";
			break;
		case 2: 
			if(Language=="Portugues") instruction="Modo Pratica\n\n"+
				"Objetivo: Eliminar todos os alvos animados em forma de zumbi o mais rapido possivel!\n\n" +
				"Regras:\n" +
				"- Seus unicos equipamentos disponiveis sao a sua arma e forca de vontade.\n" +
				"- Tiros na cabeca (headshots) dao bonus de 3 segundos cada.\n" +
				"- Atirar em alvos de pessoas normais implica em uma penalidade de 10 segundos por acerto!\n";
		
			else instruction="Practice Mode\n\n"+
				"Objective: Eliminate all zombie-like animated dummies in the area as fast as you can!\n\n" +
				"Rules:\n" +
				"- Your only equipments available are your weapon and willpower.\n" +
				"- Headshots give a 3 second bonus each.\n" +
				"- Shooting at regular human dummies implies in a 10 second penalty per hit!\n";
			break;
		case 3: 
			if(Language=="Portugues") instruction="Modo Extremo\n\n"+
				"Objetivo: Defenda-se das hordas de zumbis antes que te alcancem!\n\n" +
				"Regras:\n" +
				"- Seus unicos equipamentos disponiveis sao a sua arma e forca de vontade.\n" +
				"- Tiros na cabeca (headshots) restauram uma vida cada.\n" +
				"- Voce perde uma vida quando um zumbi encosta em voce. Se perder todas as vidas, fim de jogo!\n";
			
			else instruction="Extreme Mode\n\n"+
				"Objective: Defend yourself from the zombie hordes before they reach you!\n\n" +
				"Rules:\n" +
				"- Your only equipments available are your weapon and willpower.\n" +
				"- Headshots restore one life each.\n" +
				"- You lose one life if a zombie touches you. Lose all lives and game over!\n";
			break;
		}
	}
	
	void Settings()//Configruações
	{
		GUI.Box(new Rect(0,0,Screen.width,Screen.height), settingsText,title.box);
		PageTurner();
		
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
			Language, levelSkin.button))
		{
			if(Language=="English") Language="Portugues";
			else if (Language=="Portugues") Language="English";
		}
	}
	
	void LevelSelect()//Seleção de fases
	{		
		GUI.Box(new Rect(0,0,Screen.width,Screen.height), choosemapText,levelSkin.box);
		PageTurner();
		
		//Level List Start//
		GUI.BeginGroup(new Rect(Screen.width*0.1f, Screen.height*0.15f,Screen.width*0.3f,Screen.height*0.7f));

			//Practice Mode
		GUILayout.Label (practiceText);

		if(GUILayout.Button(new GUIContent("Green Building","Green Building"),levelSkin.button))
		{
			Application.LoadLevel("greenbuilding"); 
			currentScene="loading";
		}
		if(GUILayout.Button(new GUIContent("Wasteland","Wasteland"),levelSkin.button))
		{
			Application.LoadLevel("wasteland"); 
			currentScene="loading";
		}

			//Extreme Mode
		GUILayout.Label (extremeText);

		if(GUILayout.Button(new GUIContent("Wasteland Extreme","Wasteland Extreme"),levelSkin.button))
		{
			Application.LoadLevel("wastelandExtreme"); 
			currentScene="loading";
		}		
				
		GUI.EndGroup();
		//Level List End//
		
		//Mostra um preview da fase ao passar o mouse sobre o botão.
		switch(GUI.tooltip)
		{
		case "Green Building":
			currentPreview=greenbuilding;
			levelName="Green Building";
			authorName="ERLHN";
			break;
		case "Wasteland":
			currentPreview=wasteland;
			levelName="Wasteland";
			authorName="Rafael Alves";
			break;		
		case "Wasteland Extreme":
			currentPreview=wastelandExtreme;
			levelName="Wasteland Extreme";
			authorName="Rafael Alves";
			break;		
		default:
			currentPreview=preview;
			levelName="";
			authorName="";
			break;
		}
		
		//Define a posição do preview, além de enviar o nome da fase ao Score
		GUI.BeginGroup(new Rect(Screen.width*0.45f,Screen.height*0.2f,400,Screen.height*0.75f));
		
		GUI.Label(new Rect(0,0,400,240),currentPreview);
		if(levelName!="")
		{
			GUI.Label(new Rect(0,250,400,levelSkin.label.fontSize*2.5f), levelName+"\n"+byText+authorName,levelSkin.label);
			Score.mapName=levelName;//Envia o nome da fase ao score.
		}
		
		GUI.EndGroup();
	}
}
