using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour {

	public int speed = 200;
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.RotateTowards (transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles + new Vector3 (0, 90, 0)), speed * Time.deltaTime);
	}
}
