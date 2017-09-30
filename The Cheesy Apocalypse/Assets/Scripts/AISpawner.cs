using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

	public GameObject ai;
	Transform[] spoints;

	// Use this for initialization
	void Start () {
		spoints = GetComponentsInChildren<Transform> ();
		Invoke ("SpawnEnemy", 1f);
	}
	

	void SpawnEnemy (){
		Transform rndPos = GetRandomSpawnPoint ();
		Instantiate (ai, rndPos.position, rndPos.rotation);
		Invoke ("SpawnEnemy", Random.Range(5f, 10f));
	}

	Transform GetRandomSpawnPoint (){
		return spoints [Random.Range (0, spoints.Length)];
	}
}
