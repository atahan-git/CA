using System.Collections;
using UnityEngine;
using System;

public class Health : MonoBehaviour, IDamageable
{
	public static Health s;

	public Action chaseCheese;

    RigidFPC MovementScript;

    Rigidbody PlayerRigidbody;

    public GameObject Cheese;
	public GameObject activeCheese; //this should be a reference to the currently dropped cheese

    public int healthPoints = 1;

    public bool haveCheese = true;

    void Start()
    {
		s = this;
        MovementScript = GetComponent<RigidFPC>();
        PlayerRigidbody = GetComponent<Rigidbody>();
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
			activeCheese = (GameObject)Instantiate(Cheese, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);
			//call this only when we actually drop a piece of cheese
			chaseCheese.Invoke ();

            PlayerRigidbody.velocity = Vector3.zero;
            PlayerRigidbody.angularVelocity = Vector3.zero;
        }

        MovementScript.enabled = false;

        Invoke("RemoveStun", 1);
    }

    void RemoveStun()
    {
        MovementScript.enabled = true;
    }
}
