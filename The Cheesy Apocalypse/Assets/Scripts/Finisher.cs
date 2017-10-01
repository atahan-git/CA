using System.Collections;
using UnityEngine;

public class Finisher : MonoBehaviour
{
    public int CurrentLevel;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponentInParent<Health>().haveCheese)
        {
            //LevelSelector.s.SelectLevel(0);
			Win ();
        }
        if (other.tag == "Enemy" && other.GetComponent<AI_Movement>().haveCheese)
        {
            //LevelSelector.s.SelectLevel(CurrentLevel);
			Lose ();
        }
    }

	void Win (){
		LevelEndVisuals.s.Win ();
	}

	void Lose (){
		LevelEndVisuals.s.Lose ();
	}
}
