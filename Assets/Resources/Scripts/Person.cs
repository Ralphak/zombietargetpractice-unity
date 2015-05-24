using UnityEngine;
using System.Collections;

public class Person : MonoBehaviour 
{
	public GameObject selfExplosion;
	public AudioClip explosionSound;
	
	//Diz ao Score que existe mais uma pessoa no jogo
	void Start ()
	{
		Score.peopleOnMap++;
		Score.peopleAlive++;		
	}
	
	//Use essa função para "matar" a pessoa e retirá-la do score
	void Kill()
	{
		Score.peopleAlive--;
		HUD.wrongFlash=true;
		AudioSource.PlayClipAtPoint(explosionSound,transform.position,0.9f);
		Instantiate(selfExplosion,transform.position+Vector3.up*5,transform.rotation);
		Destroy(gameObject);
	}	
}
