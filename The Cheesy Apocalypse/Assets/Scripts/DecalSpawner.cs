using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSpawner : MonoBehaviour {

	//use the box collider to define an area

	public GameObject[] toSpawn = new GameObject[1];
	public Vector2 amount = new Vector2 (2, 6);

	// Use this for initialization
	void Start () {
		Vector3 area = GetComponent<BoxCollider> ().size;
		GetComponent<BoxCollider> ().enabled = false;
		int am = (int)Random.Range (amount.x, amount.y);

		for (int i = 0; i <= am; i++) {
			Instantiate(toSpawn[Random.Range(0,toSpawn.Length)],
				new Vector3(transform.position.x + Random.Range(-area.x/2f,area.x/2f), transform.position.y, transform.position.z + Random.Range(-area.z/2f,area.z/2f)),
				Quaternion.Euler(0,Random.Range(0,360),0));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
