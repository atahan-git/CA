using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    RigidFPC MovementScript;

    Rigidbody PlayerRigidbody;

    public GameObject Cheese;

    public int healthPoints = 1;

    public bool haveCheese = true;

    void Start()
    {
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
            Instantiate(Cheese, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), transform.rotation);

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
