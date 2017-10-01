using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pauser : MonoBehaviour {

	public GameObject pScreen;

	// Use this for initialization
	void Start () {
		
	}

	bool isPause = false;
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
			if (!isPause) {
				Time.timeScale = 0;
				isPause = true;
				pScreen.SetActive (true);
			} else {
				Time.timeScale = 1;
				isPause = false;
				pScreen.SetActive (false);
			}
		}
	}

	public void UnPause (){
		Time.timeScale = 1;
		isPause = false;
		pScreen.SetActive (false);
	}

	public void BackToMenu (){
		LevelSelector.s.SelectLevel (0);
	}
}
