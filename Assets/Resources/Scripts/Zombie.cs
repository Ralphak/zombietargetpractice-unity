//Esta classe define o comportamento dos zumbis

using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour
{
	public bool isExtreme;
	public GameObject selfExplosion;
	public AudioClip explosionSound;
	public AudioClip wrongSound;
	Transform Player;
	bool wrong;
	bool isTriggered;
	
	void Start ()
	{
		if(gameObject.name!="Zombie:Head") //Isso evita que o jogo também conte a cabeça do zumbi
		{
			Score.zombieCounter++;
			Score.zombiesOnMap++;
		}
	}
	
	void Update()
	{
		if(isExtreme && Time.timeScale!=0)
		{
			Player=CharacterMotor.tr;
			transform.LookAt(Player);
			transform.Translate(Vector3.forward*0.7f);
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if(other.collider.tag=="Player")
		{
			wrong=true;
			Score.lifeCounter--;
			Score.wrongFlash=true;
			Kill();
		}
	}
	
	//Use essa função para "matar" o zumbi e atualizar o score
	void Kill()
	{
		if(Score.zombieCounter==1) Score.currentWave++;
		
		Score.zombieCounter--;
		
		if(wrong) AudioSource.PlayClipAtPoint(wrongSound,transform.position,0.9f);
		else Score.zombiesHit++;
		
		AudioSource.PlayClipAtPoint(explosionSound,transform.position,0.9f);
		
		if(gameObject.name=="Zombie:Head")
		{
			if(!wrong)
			{
				Score.headshots++;
				Score.headshotFlash=true;
			}
			if(Score.lifeCounter<2 && Score.lifeCounter>0) Score.lifeCounter++;
			Instantiate(selfExplosion,transform.position-Vector3.up*5,transform.rotation);
			Destroy(transform.root.gameObject);
		}
		else 
		{
			Instantiate(selfExplosion,transform.position+Vector3.up*5,transform.rotation);
			Destroy(gameObject);
			
			if(isTriggered) 
			{
				BackWarning.zombiesInTrigger--;
				isTriggered=false;
			}
		}
	}
	
	void OnTriggerEnter(Collider trigger)
	{
		if(!isTriggered && trigger.name=="WarningTrigger")
		{ 
			BackWarning.zombiesInTrigger++;
			isTriggered=true;
		}
	}
	void OnTriggerExit(Collider trigger)
	{
		if(isTriggered && trigger.name=="WarningTrigger")
		{ 
			BackWarning.zombiesInTrigger--;
			isTriggered=false;
		}
	}
}

