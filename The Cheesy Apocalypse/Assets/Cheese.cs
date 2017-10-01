using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (Health.s.chaseCheese != null)
		{
			AI_Movement.distance = 10000;
			Health.s.chaseCheese.Invoke ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
