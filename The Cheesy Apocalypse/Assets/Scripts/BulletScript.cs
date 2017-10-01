using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	BoxCollider myCol;
	bool isLethal = true;
	Renderer rend;

	TrailRenderer myTrail;

    public float projectileSpeed = 600;

	public bool isRocket = false;

	public GameObject rocketExp;

	float rocketExpRange = 2f;

	void Start () {
		myCol = GetComponent<BoxCollider> ();
		myTrail = GetComponentInChildren<TrailRenderer> ();
		rend = GetComponentInChildren<Renderer> ();
		//rend.material.color = new Color (0.2f, 0.2f, 0.2f);

		myCol.enabled = false;
		Invoke ("EnableCollider", 0.05f);

	    GetComponent<Rigidbody> ().AddRelativeForce (0, 0, projectileSpeed);
	}

	void EnableCollider (){
		myCol.enabled = true;
	}
	
	void FixedUpdate () {
		if (isLethal) {
			GetComponent<Rigidbody> ().AddForce (0f, Physics.gravity.y / 5f, 0f);
		} else {
			GetComponent<Rigidbody> ().AddForce (0f, Physics.gravity.y, 0f);
		}


		if (GetComponent<Rigidbody> ().velocity.magnitude < 5f && myCol.enabled) {
			isLethal = false;
			myTrail.enabled = false;
			rend.material.color = new Color (0.6f, 0.6f, 0.6f);
		}
	}

	void OnCollisionEnter (Collision col){
		if (isLethal) {
			if (col.gameObject.tag != "Bullet") {
				IDamageable myTarget = GetDamageable (col.collider.gameObject);

				if (isRocket) {
					Collider[] overlap = Physics.OverlapSphere (transform.position, rocketExpRange);
					foreach (Collider _col in overlap) {
						IDamageable _tar = GetDamageable (_col.gameObject);
						if (_tar != null)
							_tar.Damage ();
					}
				}

				if (myTarget != null) {
					myTarget.Damage ();
				}
			}
		}
	}

	IDamageable GetDamageable (GameObject obj){
		IDamageable myThing;
		//obj = obj.transform.root.gameObject;

		myThing = obj.GetComponent<IDamageable> ();

        if (myThing == null)
            myThing = obj.GetComponentInParent<IDamageable> ();
        if(myThing == null)
            myThing = obj.GetComponentInChildren<IDamageable>();

        return myThing;
	}
}
