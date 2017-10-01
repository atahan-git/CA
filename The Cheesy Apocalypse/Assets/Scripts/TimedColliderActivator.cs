using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedColliderActivator : MonoBehaviour {

	public float time = 0.1f;

	// Use this for initialization
	void Start () {
		Disable ();
		Invoke ("Activate", time);
	}

	void Disable (){
		foreach (Collider col in gameObject.GetComponentsInChildren<Collider>()) {
			col.enabled = false;
		}
	}
	

	void Activate (){
		foreach (Collider col in gameObject.GetComponentsInChildren<Collider>()) {
			col.enabled = true;
		}
	}
}
