using System.Collections;
using UnityEngine;

public class Finisher : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponentInParent<Health>().haveCheese)
        {
            LevelSelector.s.SelectLevel(0);
        }
        if (other.tag == "Enemy" && other.GetComponent<AI_Movement>().haveCheese)
        {
            LevelSelector.s.SelectLevel(1);
        }
    }
}
