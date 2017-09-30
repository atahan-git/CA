using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform barrelPos;

    public GameObject bullet;

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
	}

    void Shoot()
    {
        Instantiate(bullet, barrelPos.position, barrelPos.rotation);
    }
}
