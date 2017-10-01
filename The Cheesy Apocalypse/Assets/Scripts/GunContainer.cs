using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour {

	public static GunContainer s;

	public GameObject[] container;

	public GameObject bullet;
	public GameObject sniperBullet;
	public GameObject rocketBullet;

	// Use this for initialization
	void Awake () {
		s = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
