using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidFPC : MonoBehaviour {

	//speed x= forward, y=jump, z=sideways
	private Vector3 speed = new Vector3(6, 4, 5);
	Vector3 runSpeed = new Vector3 (6, 5, 5);
	Vector3 normalSpeed = new Vector3 (4, 4, 3.5f);
	Vector3 crouchSpeed = new Vector3 (2, 3, 2);

	Transform cam;//our camera
	bool grounded;//are we touching ground?
	//var airSpeedSlowAmount = 0;
	float sidewaysMovementSlower = 1.1f;// how slow should sideway movement be? higher -> slower
	float movementStartSlowAmount = 1.2f;//how slowly should we start to move? higher -> slower
	float backwardsSlower = 1.7f;// how slow should backwards movement be? higher -> slower
	float cameraJiggleAmount = 3f;//how fast shoul it jiggle according to speed
	float fallDamageMultipler = 20;//how much damage should we deal? multiplied by velocity
	float fallDamgeMinH = 8;//minimun velocity to receive fall damage

	void FixedUpdate () {

		//run and "crouch" change speed values
		if(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)){
			speed = crouchSpeed;

		}else if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
			speed = runSpeed;

		}else{
			speed = normalSpeed;

		}


		//---movement---

		//Get input and multiply them with speed values
		Vector3 Keyinput = new Vector3(Input.GetAxis("Horizontal") * speed.z, GetComponent<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * speed.x);

		//while we are touching ground
		if(grounded == true){

			//if we are moving in two directions make it slower to stop being able to diagonaly move faster
			if(Keyinput.x != 0 && Keyinput.z != 0) {
				Keyinput.x /= sidewaysMovementSlower;
				Keyinput.z /= sidewaysMovementSlower;			
			}
			//slower backwards
			if(Keyinput.z < 0) Keyinput.z /= backwardsSlower;
			//camera jiggle.
			//CameraJiggle(Keyinput);
			//calculate the velocity(locvel) we gonna add
			var locVel = transform.TransformDirection(Keyinput); //transform local directions to world directions
			locVel = locVel - GetComponent<Rigidbody>().velocity; //umm
			locVel /= movementStartSlowAmount; //make it start slowly
			//add our velocity
			GetComponent<Rigidbody>().velocity += locVel;

			//while we are in the air
		}/*else{
   	
   		//calculate the force(ForceToAdd) we gonna add
   		var ForceToAdd = Vector3(Keyinput.x, 0, Keyinput.z);
   		ForceToAdd /= airSpeedSlowAmount;
   		
   		//add our force
   		rigidbody.AddRelativeForce(ForceToAdd);
   		
   	}*/

		//jump
		if(Input.GetKeyDown(KeyCode.Space) && grounded && GetComponent<Rigidbody>().velocity.y < speed.y - 1){
			GetComponent<Rigidbody> ().velocity += new Vector3 (0, speed.y, 0);
		}

	}


	/*void CameraJiggle (Keyinput : Vector3) {

		//if we are moving
		if(Keyinput.x != 0 || Keyinput.z != 0){
			cameraAnim.SetBool("isWalking", true); //set the animation

		}else{
			cameraAnim.SetBool("isWalking", false);
		}
		//calculate animation speed
		var animSpeed =  Keyinput.z/cameraJiggleAmount;
		if(animSpeed >= 0) animSpeed += 0.7f;
		else animSpeed = 1f;
		//change animation speed according to our velocity
		cameraAnim.speed = animSpeed;


	}*/

	void OnTriggerEnter (){
		//fall damage
		//check velocity
		if(GetComponent<Rigidbody>().velocity.y > fallDamgeMinH){
			//damage more according to the velocity
			//health.Damage(rigidbody.velocity.y * fallDamageMultipler);

		}

	}
	//are we touching ground?
	void OnTriggerStay () {
		grounded = true;
	}

	void OnTriggerExit () {
		//we have jumped or falled
		grounded = false;
		//cameraAnim.SetBool("isWalking", false);
	}


}
