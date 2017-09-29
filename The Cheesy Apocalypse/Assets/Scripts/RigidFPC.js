#pragma strict
//speed x= forward, y=jump, z=sideways
private var speed = Vector3(6,4,5);
var runSpeed = Vector3(6,5,5);
var normalSpeed = Vector3(4,4,3.5);
var crouchSpeed = Vector3(2,3,2);

var cam : Transform;//our camera
var grounded : boolean;//are we touching ground?
//var airSpeedSlowAmount = 0;
var sidewaysMovementSlower = 1.1f;// how slow should sideway movement be? higher -> slower
var movementStartSlowAmount = 1.2f;//how slowly should we start to move? higher -> slower
var backwardsSlower = 1.7;// how slow should backwards movement be? higher -> slower
var cameraAnim : Animator;//camera jiggle animator
var cameraJiggleAmount = 3f;//how fast shoul it jiggle according to speed
var fallDamageMultipler = 20;//how much damage should we deal? multiplied by velocity
var fallDamgeMinH = 8;//minimun velocity to receive fall damage
var health : Health; //health component for fall damage
function Start (){
	
}

function FixedUpdate () {

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
	var Keyinput = new Vector3(Input.GetAxis("Horizontal") * speed.z, GetComponent.<Rigidbody>().velocity.y, Input.GetAxis("Vertical") * speed.x);

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
		CameraJiggle(Keyinput);
   		//calculate the velocity(locvel) we gonna add
   		var locVel = transform.TransformDirection(Keyinput); //transform local directions to world directions
   		locVel = locVel - GetComponent.<Rigidbody>().velocity; //umm
   		locVel /= movementStartSlowAmount; //make it start slowly
   		//add our velocity
		GetComponent.<Rigidbody>().velocity += locVel;
		
	//while we are in the air
   	}/*else{
   	
   		//calculate the force(ForceToAdd) we gonna add
   		var ForceToAdd = Vector3(Keyinput.x, 0, Keyinput.z);
   		ForceToAdd /= airSpeedSlowAmount;
   		
   		//add our force
   		rigidbody.AddRelativeForce(ForceToAdd);
   		
   	}*/
   	
	//jump
	if(Input.GetKeyDown(KeyCode.Space) && grounded && GetComponent.<Rigidbody>().velocity.y < speed.y - 1){
		GetComponent.<Rigidbody>().velocity.y += speed.y;
	}
	
}


function CameraJiggle (Keyinput : Vector3) {

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

		
}

function OnTriggerEnter (){
	//fall damage
	//check velocity
	if(GetComponent.<Rigidbody>().velocity.y > fallDamgeMinH){
		//damage more according to the velocity
		//health.Damage(rigidbody.velocity.y * fallDamageMultipler);
		
	}

}
//are we touching ground?
function OnTriggerStay () {
		grounded = true;
}

function OnTriggerExit () {
	//we have jumped or falled
	grounded = false;
	cameraAnim.SetBool("isWalking", false);
}

