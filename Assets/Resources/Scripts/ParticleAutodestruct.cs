//Autodestruction for new particle system

using UnityEngine;
using System.Collections;

public class ParticleAutodestruct : MonoBehaviour {void Update () {Destroy(gameObject, GetComponent<ParticleSystem>().duration+0.5f);}}

