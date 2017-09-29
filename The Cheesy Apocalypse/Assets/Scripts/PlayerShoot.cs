using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform barrelPos;

    public GameObject bullet;

    void Update()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
	}

    void Shoot()
    {
        Instantiate(bullet, barrelPos.position, barrelPos.rotation);
        Invoke("Shoot", Random.Range(1f, 3f));
    }
}
