using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEndVisuals : MonoBehaviour {

	public static LevelEndVisuals s;

	public GameObject winScreen;
	public GameObject loseScreen;

	// Use this for initialization
	void Start () {
		s = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	bool isLocked = false;

	public void Win (){
		if (isLocked)
			return;
		isLocked = true;

		GameObject.FindGameObjectWithTag ("Player").transform.root.GetComponent<RigidFPC> ().enabled = false;
		Time.timeScale = 0;

		winScreen.SetActive (true);
		loseScreen.SetActive (false);

		PlayerPrefs.SetInt (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex.ToString (), 1);
	}

	public void Lose (){
		if (isLocked)
			return;
		isLocked = true;

		GameObject.FindGameObjectWithTag ("Player").transform.root.GetComponent<RigidFPC> ().enabled = false;
		Time.timeScale = 0;

		winScreen.SetActive (false);
		loseScreen.SetActive (true);
	}

	public void BackToMenu (){
		LevelSelector.s.SelectLevel (0);
	}

	public GameObject oldTxt;
	public GameObject newTxt;
	public Button nb;
	public void NextLevel (){
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex < 4) {
			Time.timeScale = 1;
			LevelSelector.s.SelectLevel (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().buildIndex + 1);
		} else {
			oldTxt.SetActive (false);
			newTxt.SetActive (true);
			nb.interactable = false;
		}
	}
}
