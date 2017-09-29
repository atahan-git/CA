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
	

	void Shoot (){

	}
}
