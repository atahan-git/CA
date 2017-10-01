using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	BoxCollider myCol;
	bool isLethal = true;
	Renderer rend;

	TrailRenderer myTrail;

	public bool isSnipar = false;

	// Use this for initialization
	void Start () {
		myCol = GetComponent<BoxCollider> ();
		myTrail = GetComponentInChildren<TrailRenderer> ();
		rend = GetComponentInChildren<Renderer> ();
		rend.material.color = new Color (0.4f, 0.4f, 0.4f);

		myCol.enabled = false;
		Invoke ("EnableCollider", 0.05f);

		if(!isSnipar)
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, 600);
		else
			GetComponent<Rigidbody> ().AddRelativeForce (0, 0, 1500);
	}

	void EnableCollider (){
		myCol.enabled = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isLethal) {
			GetComponent<Rigidbody> ().AddForce (0f, Physics.gravity.y / 5f, 0f);
		} else {
			GetComponent<Rigidbody> ().AddForce (0f, Physics.gravity.y, 0f);
		}
	}

	void OnCollisionEnter (Collision col){
		if (isLethal) {
			isLethal = false;
			myTrail.enabled = false;
			rend.material.color = new Color (0.6f, 0.6f, 0.6f);

			IDamageable myTarget = GetDamageable (col.collider.gameObject);

			if (myTarget != null) {
				myTarget.Damage ();
			}
		}
	}

	IDamageable GetDamageable (GameObject obj){
		IDamageable myThing;
		obj = obj.transform.root.gameObject;

		myThing = obj.GetComponent<IDamageable> ();
		myThing = obj.GetComponentInChildren<IDamageable> ();
		myThing = obj.GetComponentInParent<IDamageable> ();

		return myThing;
	}
}
