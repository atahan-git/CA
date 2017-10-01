using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunContainer : MonoBehaviour {

	public static GunContainer s;

	public GameObject[] container;

	public GameObject bullet;
    public GameObject shotgunBullet;
	public GameObject sniperBullet;
	public GameObject rocketBullet;

	void Awake ()
    {
		s = this;
	}
}
