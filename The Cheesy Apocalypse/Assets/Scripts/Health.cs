using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float heatlhPoints = 100;

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

	}
}
