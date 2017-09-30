using System.Collections;
using UnityEngine;

public class Finisher : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<Health>().haveCheese)
        {
            print("GG, You Win!");
        }
        if (other.tag == "Enemy" && other.GetComponent<AI_Movement>().haveCheese)
        {
            print("You Lost!");
        }
    }
}
