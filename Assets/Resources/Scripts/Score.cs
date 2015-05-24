//Essa classe recolhe informações do jogo para enviá-las à tela de resultados

using UnityEngine;
using System.Collections;

public static class Score
{
	public static string mapName;
	public static float timeTaken;
	public static int zombieCounter;
	public static int zombiesOnMap;
	public static int zombiesHit;
	public static float shotsFired;
	public static int headshots;
	public static int peopleOnMap;
	public static int peopleAlive;
	public static int currentWave=1;
	public static int lifeCounter=2;
	public static bool headshotFlash;
	public static bool wrongFlash;
	
	//Limpa todos os dados para a próxima partida
	public static void Reset()
	{
		mapName=null;
		timeTaken=0;
		peopleAlive=0;
		peopleOnMap=0;
		zombieCounter=0;
		zombiesOnMap=0;
		zombiesHit=0;
		shotsFired=0;
		headshots=0;
		currentWave=1;
		lifeCounter=2;
		headshotFlash=false;
		wrongFlash=false;
		BackWarning.zombiesInTrigger=0;
	}
}
