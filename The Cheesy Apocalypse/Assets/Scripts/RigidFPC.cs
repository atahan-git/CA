using UnityEngine;

public class RigidFPC : MonoBehaviour
{
	public Vector3 Speed;

    public GameObject Visuals;

	public float SidewaysMovementContoller;
    public float StartingSmoothness;

    bool Grounded;

    

    void FixedUpdate()
    {
        Vector3 Keyinput = new Vector3(Input.GetAxis("Horizontal") * Speed.z, GetComponent<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * Speed.x);

        if (Keyinput.x != 0 && Keyinput.z != 0)
        {
            Keyinput.x /= SidewaysMovementContoller;
            Keyinput.z /= SidewaysMovementContoller;
        }

        Vector3 LocalVelocity = transform.TransformDirection(Keyinput);
        LocalVelocity = LocalVelocity - GetComponent<Rigidbody>().velocity;
        LocalVelocity /= StartingSmoothness;
        GetComponent<Rigidbody>().velocity += LocalVelocity;

        if (Input.GetKeyDown(KeyCode.Space) && Grounded && GetComponent<Rigidbody>().velocity.y < Speed.y - 1)
        {
            GetComponent<Rigidbody>().velocity += new Vector3(0, Speed.y, 0);
        }

        //visuals
        Vector3 LookPosition = Vector3.zero;

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            LookPosition.z = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            LookPosition.z = -1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LookPosition.x = -1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            LookPosition.x = 1;
        }

        Visuals.transform.LookAt(LookPosition + Visuals.transform.position);
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
