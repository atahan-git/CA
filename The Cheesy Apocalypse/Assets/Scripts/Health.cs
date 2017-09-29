using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    RigidFPC MovementScript;

    public GameObject Cheese;

    public int healthPoints = 1;

    public bool haveCheese = true;

    void Start()
    {
        MovementScript = GetComponent<RigidFPC>();
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
        }

        MovementScript.enabled = false;

        Invoke("RemoveStun", 1);
    }

    void RemoveStun()
    {
        MovementScript.enabled = true;
    }
}
