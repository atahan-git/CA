using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public GameObject Player;

    Health HealthScript;

	void Update()
    {
		if(Input.GetKeyDown(KeyCode.O))
        {
            HealthScript = Player.GetComponent<Health>();
            HealthScript.Damage();
        }
	}
}
