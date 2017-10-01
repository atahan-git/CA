using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour {

	public static LevelReset s;

	// Use this for initialization
	void Awake () {
		s = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R))
			Reset ();
	}

	void Reset (){
		LevelSelector.s.SelectLevel (SceneManager.GetActiveScene ().buildIndex);
	}
}
