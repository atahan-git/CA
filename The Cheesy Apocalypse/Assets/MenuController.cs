using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject mainButton;
	public Image bgimage;
	public GameObject menu1;

	public Button[] levelButtons;

	public Color bgnormal = Color.white;
	public Color bgmenu = Color.grey;
	public Color startcolor = Color.black;
	Color oldColor = Color.black;
	Color activeColor = Color.white;
	float colorVal = 0f;

	void Start (){
		activeColor = bgnormal;

		menu1.SetActive (false);
		bgimage.color = startcolor;

		for (int i = 1; i <= 5; i++) {
			if (PlayerPrefs.GetInt (i.ToString (), 0) == 0) {
				if(i < levelButtons.Length)
				levelButtons [i].interactable = false;
			}
		}
	}

	void Update (){
		bgimage.color = Color.Lerp (oldColor, activeColor, colorVal);

		colorVal += 1f * Time.deltaTime;

		if (colorVal > 1f)
			colorVal = 1f;
	}

	public void MainButton (){
		print ("this called");
		activeColor = bgmenu;
		oldColor = bgnormal;
		colorVal = 0f;
		mainButton.GetComponent<Animator> ().SetBool ("isDone", true);
		Invoke ("ActivateMenu",0.5f);
	}

	void ActivateMenu (){
		menu1.SetActive (true);
	}

	public void LevelSelect (int id){
		LevelSelector.s.SelectLevel (id);
	}
}
