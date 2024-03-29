﻿using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
	public static Health s;
	public System.Action chaseCheese;
	public System.Action stopChase;

    RigidFPC movementScript;

    Rigidbody playerRigidbody;

    public GameObject Cheese;
    public GameObject MyCheese;
    public GameObject StunSymbol;

    [HideInInspector]
    public GameObject activeCheese;

    public int healthPoints = 1;

    public bool haveCheese = true;

	void Awake(){
		s = this;
	}

    void Start()
    {
        movementScript = GetComponent<RigidFPC>();
        playerRigidbody = GetComponent<Rigidbody>();
        StunSymbol.SetActive(false);
    }

    public void Damage()
    {
        Damage(1);
    }

    public void Damage(int amount)
    {
		healthPoints -= amount;

        if (healthPoints <= 0)
        {
            if(haveCheese)
            {
                activeCheese = (GameObject)Instantiate(Cheese, transform.position, transform.rotation);
                Vector3 shootVector = Quaternion.Euler(0, Random.Range(0, 360), 0) * new Vector3(150, 400, 0);
                activeCheese.GetComponent<Rigidbody>().AddForce(shootVector);

                activeCheese.GetComponentInChildren<MeshCollider>().enabled = false;
                Invoke("EnableCollider", 0.3f);


                playerRigidbody.AddForce(0, 200, 0);

                haveCheese = false;
                MyCheese.SetActive(false);
            }

            playerRigidbody.velocity = Vector3.zero;
            playerRigidbody.angularVelocity = Vector3.zero;
            StunSymbol.SetActive(true);
        }

        movementScript.enabled = false;
        Invoke("RemoveStun", 1);
    }

    void EnableCollider()
    {
        activeCheese.GetComponentInChildren<MeshCollider>().enabled = true;
    }

    void RemoveStun()
    {
        movementScript.enabled = true;
        StunSymbol.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cheese") && movementScript.enabled)
        {
            Destroy(collision.gameObject);
            haveCheese = true;
            MyCheese.SetActive(true);
			if(stopChase != null)
			stopChase.Invoke ();
        }
    }
}
