using UnityEngine;

public class RigidFPC : MonoBehaviour
{
	public Vector3 speed;

    public GameObject visuals;

	public float sidewaysMovementContoller;
    //public float startingSmoothness;
    public float cheeseSpeed;

    bool grounded;

    

    void FixedUpdate()
    {
        Vector3 keyinput = new Vector3(Input.GetAxis("Horizontal") * speed.z, GetComponent<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * speed.x);

        if (keyinput.x != 0 && keyinput.z != 0)
        {
            keyinput.x /= sidewaysMovementContoller;
            keyinput.z /= sidewaysMovementContoller;
        }

        Vector3 localVelocity = transform.TransformDirection(keyinput);
        localVelocity = localVelocity - GetComponent<Rigidbody>().velocity;
        //localVelocity /= startingSmoothness;
        if (GetComponent<Health>().haveCheese)
        {
            localVelocity *= cheeseSpeed;
        }
        GetComponent<Rigidbody>().velocity += localVelocity;

        if (Input.GetKeyDown(KeyCode.Space) && grounded && GetComponent<Rigidbody>().velocity.y < speed.y - 1)
        {
            GetComponent<Rigidbody>().velocity += new Vector3(0, speed.y, 0);
        }

        //visuals
        Vector3 lookPosition = Vector3.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            lookPosition.z = -1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            lookPosition.z = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            lookPosition.x = 1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            lookPosition.x = -1;
        }

        visuals.transform.LookAt(lookPosition + visuals.transform.position);
        if(lookPosition.magnitude > 0)
        visuals.transform.Rotate(0, -45, 0);
    }

	void OnTriggerStay()
    {
		grounded = true;
	}

	void OnTriggerExit()
    {
		grounded = false;
	}
}
