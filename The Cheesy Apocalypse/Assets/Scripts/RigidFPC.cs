using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFPC : MonoBehaviour
{
	public Vector3 Speed = new Vector3(6, 4, 5);

	public float SidewaysMovementContoller = 1.1f;
    public float StartingSmoothness = 1.2f;
    public float BackwardsSlower = 1.7f;

    bool Grounded;

    void FixedUpdate()
    {
		if(Grounded)
        {
            Vector3 Keyinput = new Vector3(Input.GetAxis("Horizontal") * Speed.z, GetComponent<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * Speed.x);

            if (Keyinput.x != 0 && Keyinput.z != 0)
            {
				Keyinput.x /= SidewaysMovementContoller;
				Keyinput.z /= SidewaysMovementContoller;			
			}

            if(Keyinput.z < 0)
            {
                Keyinput.z /= BackwardsSlower;
            }

			Vector3 LocalVelocity = transform.TransformDirection(Keyinput);
			LocalVelocity = LocalVelocity - GetComponent<Rigidbody>().velocity;
			LocalVelocity /= StartingSmoothness;
			GetComponent<Rigidbody>().velocity += LocalVelocity;

            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<Rigidbody>().velocity.y < Speed.y - 1)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, Speed.y, 0);
            }
        }
	}

	void OnTriggerStay()
    {
		Grounded = true;
	}

	void OnTriggerExit()
    {
		Grounded = false;
	}
}
