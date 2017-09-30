using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour, IDamageable {

	public int heatlhPoints = 1;

	public GameObject deathEffect;


	public void Damage (){
		Damage (1);
	}

	public void Damage (int amount){
		heatlhPoints -= amount;

		if (heatlhPoints <= 0)
			BroadcastMessage ("Die");
	}

	void Die(){
		Instantiate (deathEffect, transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}


public interface IDamageable{
	void Damage ();
	void Damage (int amount);
}