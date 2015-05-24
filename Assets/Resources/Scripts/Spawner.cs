using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	
	public GameObject objectToSpawn;
	int spawnLeft;
	int lastWave;
	bool spawning;
	float spawnDelay;
	float spawnStart;

	void Update () 
	{
		//Garante que o contador não atinja valores bizarros
		if(lastWave!=Score.currentWave)
		{
			spawnLeft=(int)Mathf.Pow(2,Score.currentWave);
			lastWave=Score.currentWave;
		}
		
		if(spawnLeft>0 && spawning)
		{
			spawnDelay++;
			if(spawnDelay>70)
			{
				spawnLeft--;
				Instantiate(objectToSpawn,transform.position,transform.rotation);
				spawnDelay=0;
			}
		}
		else if (Score.zombieCounter==0)
		{
			spawnStart++;
			if(spawnStart>100)
			{
				spawning=true;
				spawnStart=0;
			}
		}
		else spawning=false;
	}
}
