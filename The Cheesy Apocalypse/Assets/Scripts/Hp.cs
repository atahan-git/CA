using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour {

	public float heatlhPoints = 20;

	public GameObject deathEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Damage (float amount){
		heatlhPoints -= amount;

		if (heatlhPoints <= 0)
			Die ();
	}

	void Die(){
		Instantiate (deathEffect, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
