using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Shoot : MonoBehaviour {

	Transform barrelPos;
	Transform gunPos;


	public enum ShootType {pistol, shotgun, machinegun, sniper, rocket};
	public ShootType shootType;
	int gunId = 0;

	void Start () {
		
		gunPos = transform.Find ("GunPos");

		//SpawnGun ();

		barrelPos = transform.Find ("BarrelPos");
        barrelPos.SetParent(gunPos);

		Invoke ("Shoot", 1f);


		if (shootType == ShootType.sniper)
			attackrange = 20f;
	}

	void SpawnGun (){
		if (gunPos.GetChild (0) != null)
			Destroy (gunPos.GetChild (0).gameObject);


		switch (shootType) {
		case ShootType.pistol:
			gunId = 0;
			break;
		case ShootType.shotgun:
			gunId = 1;
			break;
		case ShootType.machinegun:
			gunId = 2;
			break;
		case ShootType.sniper:
			gunId = 3;
			break;
		case ShootType.rocket:
			gunId = 4;
			break;
		}

		Instantiate (GunContainer.s.container [gunId], gunPos);
	}
		
	float attackrange = 15f;
	public void Shoot (){
		float reloadTime = 2f;
		float range = 1f;
		print ("shoot11");
		if (GetComponent<AI_Movement> ().activeMode == AI_Movement.AIMode.attackPlayer && Vector3.Distance (transform.position, GetComponent<AI_Movement> ().player.position) < attackrange) {
			print ("shoot");
			switch (shootType) {
			case ShootType.pistol:
				Instantiate (GunContainer.s.bullet, barrelPos.position, barrelPos.rotation);
				reloadTime = 2f;
				range = 1f;
				break;
			case ShootType.shotgun:
				Instantiate (GunContainer.s.bullet, barrelPos.position, barrelPos.rotation * Quaternion.Euler(0, 10f, 0));
				Instantiate (GunContainer.s.bullet, barrelPos.position, barrelPos.rotation * Quaternion.Euler(0, 0, 0));
				Instantiate (GunContainer.s.bullet, barrelPos.position, barrelPos.rotation * Quaternion.Euler(0, -10f, 0));
                Instantiate(GunContainer.s.bullet, barrelPos.position, barrelPos.rotation * Quaternion.Euler(0, 20f, 0));
                Instantiate(GunContainer.s.bullet, barrelPos.position, barrelPos.rotation * Quaternion.Euler(0, -20f, 0));
                reloadTime = 3f;
				range = 1f;
				gunId = 1;
				break;
			case ShootType.machinegun:
				StartCoroutine (MachineGunShoot());
				reloadTime = 3f;
				range = 2f;
				gunId = 2;
				break;
			case ShootType.sniper:
				Instantiate (GunContainer.s.sniperBullet, barrelPos.position, barrelPos.rotation);
				reloadTime = 4f;
				range = 1f;
				gunId = 3;
				break;
			case ShootType.rocket:
				Instantiate (GunContainer.s.rocketBullet, barrelPos.position, barrelPos.rotation);
				reloadTime = 5f;
				range = 3f;
				gunId = 4;
				break;
			}
		
		}

		Invoke ("Shoot", Random.Range (reloadTime - range, reloadTime + range));
	}

	IEnumerator MachineGunShoot(){
        for (int i = 0; i < 3; i++)
        {
            Instantiate(GunContainer.s.bullet, barrelPos.position, barrelPos.rotation);
            yield return new WaitForSeconds(Random.Range(0.1f, 0.12f));
        }
	}
}
