using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt ("M", 0) == 0) {
			GetComponent<AudioSource> ().Play ();
		} else {
			GetComponent<AudioSource> ().Pause ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.M)) {
			if (PlayerPrefs.GetInt ("M", 0) == 0) {
				PlayerPrefs.SetInt ("M", 1);
				GetComponent<AudioSource> ().Pause ();

			} else {
				PlayerPrefs.SetInt ("M", 0);
				GetComponent<AudioSource> ().Play ();
			}
		}
	}
}
