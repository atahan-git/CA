using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shoot : MonoBehaviour {

	Transform barrelPos;

	public GameObject bullet;

	// Use this for initialization
	void Start () {
		barrelPos = transform.Find ("BarrelPos");
		Invoke ("Shoot", 1f);
	}
		

	public void Shoot (){
		if(GetComponent<AI_Movement>().activeMode == AI_Movement.AIMode.attackPlayer && Vector3.Distance(transform.position, GetComponent<AI_Movement>().player.position) < 15f)
			Instantiate (bullet, barrelPos.position, barrelPos.rotation);
		
		Invoke ("Shoot", Random.Range(1f,3f));
	}
}
