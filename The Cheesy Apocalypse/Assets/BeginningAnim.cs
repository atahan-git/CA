using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningAnim : MonoBehaviour {

	public GameObject[] screens;
	int curOne = 0;

	// Use this for initialization
	void Start () {
		ActivateScreen (curOne);
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ActivateScreen(int number){
		foreach (GameObject sc in screens) {
			sc.SetActive (false);
		}

		screens [number].SetActive (true);
	}

	public void Next (){
		curOne++;
		if (curOne < screens.Length)
			ActivateScreen (curOne);
		else {
			this.gameObject.SetActive (false);
			Time.timeScale = 1;
		}
	}
}
