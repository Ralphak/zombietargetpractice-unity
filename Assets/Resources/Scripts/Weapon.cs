//Esta classe define o funcionamento da arma e seus comandos

using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public static bool aimMode;
	public CharacterMotor motor;
	public MouseLook mouseX;
	public MouseLook mouseY;
	
	public Transform bulletVector;
	public GameObject fireSpark;
	public Transform fireVector;
	public GameObject hitSpark;
	public SoundPlayer sound;
	
	Vector3 hipPosition;
	Vector3 aimPosition;	
	Vector3 aimDistance;
	float aimSpeed;
	bool aimTransition;
	Vector3 walkDistance;
	float walkSpeed;
	float moveSpeed;
	float lookX;
	float lookY;
	public static float sensitivityMultiplier=5;
	float lastMultiplier;
	public float recoil;
	
	void Start()
	{
		hipPosition=transform.localPosition;
		aimPosition= new Vector3(0,-0.13f,0.2f);
		aimDistance = hipPosition-aimPosition;
		aimDistance= Quaternion.AngleAxis(90,Vector3.up)*aimDistance; // Bug fix for magnum model
		walkDistance= new Vector3(0.015f,0.005f,0);
		aimSpeed = 0.4f;
		walkSpeed=0.2f;
		moveSpeed=motor.movement.maxSpeed;
	}
	
    void Update () 
    {
		//Garante que a sensibilidade não atinja níveis bizarros
		if(sensitivityMultiplier!=lastMultiplier && Time.timeScale!=0)
		{
			mouseX.sensitivityX=2;
			mouseY.sensitivityY=2;
			lookX = mouseX.sensitivityX * sensitivityMultiplier;
			lookY = mouseY.sensitivityY * sensitivityMultiplier;
			lastMultiplier=sensitivityMultiplier;
		}
		
		//Comando de atirar. Evita que ocorra mais de um tiro por clique.
		if(Input.GetButtonDown("Fire") && Time.timeScale!=0 && !GetComponent<Animation>().isPlaying && !aimTransition)
		{
			Score.shotsFired++;
			GetComponent<AudioSource>().Play();
			Instantiate(fireSpark,fireVector.position,fireVector.rotation);
			Raycast();
			if(aimMode)
			{
				GetComponent<Animation>().Play("magnum aimed fire");
				transform.parent.Rotate(-recoil/1.5f,0,0); //Sacode a câmera para dar a sensação de recuo
			}
			else 
			{
				GetComponent<Animation>().Play("magnum fire");
				transform.parent.Rotate(-recoil,0,0);
			}
		}		
		
		//Entra em estado de mira
		if(Input.GetButton("Aim") && Time.timeScale!=0)
		{
			aimMode=true;
			FPSInputController.aiming=true;
			motor.jumping.enabled=false;
			motor.movement.maxSpeed=moveSpeed/2.5f;
			mouseX.sensitivityX=lookX/2;
			mouseY.sensitivityY=lookY/2;
			if(transform.localPosition.x >= aimPosition.x + 0.03f) //Isso garante que a transição ocorra com fluidez
			{
				transform.Translate(aimDistance * -aimSpeed); 
				aimTransition=true;
			}
			else
			{ 
				transform.localPosition = aimPosition;
				aimTransition=false;
			}
		}
		
		//Sai da mira ao soltar o input
		else if(transform.localPosition.x <= hipPosition.x - 0.03f && Time.timeScale!=0)
		{
			transform.Translate(aimDistance);
			aimTransition=true;
		}
		
		//Movimenta a arma quando o jogador está andando
		else if(FPSInputController.directionVector!=Vector3.zero && !aimMode && Time.timeScale!=0)
		{
			transform.Translate(walkDistance * walkSpeed);
			if(transform.localPosition.y<hipPosition.y-walkDistance.y 
				|| transform.localPosition.y>hipPosition.y+walkDistance.y)
				walkSpeed*=-1;
		}
		
		//Jogador parado
		else 
		{
			transform.localPosition = hipPosition;
			aimMode=false;			
			aimTransition=false;
			FPSInputController.aiming=false;
			motor.jumping.enabled=true;
			motor.movement.maxSpeed=moveSpeed;
			mouseX.sensitivityX=lookX;
			mouseY.sensitivityY=lookY;
		}
    }	
	
	void Raycast() //Bullet Raycast
	{
		RaycastHit bulletCast;
		if(Physics.Raycast(bulletVector.position,bulletVector.forward,out bulletCast))
		{
			//Gera uma faísca onde o tiro atingiu
			if(bulletCast.collider.gameObject.name!="Boundary") 
				Instantiate(hitSpark,bulletCast.point,bulletVector.rotation);
			
			//Envia comandos quando acerta determinados alvos
			switch(bulletCast.collider.gameObject.name)
			{
			case "Zombie":				
				bulletCast.collider.gameObject.SendMessage("Kill");
				break;
			case "Zombie Extreme(Clone)":				
				bulletCast.collider.gameObject.SendMessage("Kill");
				break;
			case "Zombie:Head":
				bulletCast.collider.gameObject.SendMessage("Kill");
				if(Score.zombieCounter>0)sound.Play("headshot");				
				break;
			case "Mia":
				bulletCast.collider.gameObject.SendMessage("Kill");
				sound.Play("wrong");
				break;
			}
		}
	}
}