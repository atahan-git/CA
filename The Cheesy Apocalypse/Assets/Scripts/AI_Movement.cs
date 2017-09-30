using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;

	Transform escapeSpot;

	Transform target;

	public enum AIMode {attackPlayer, chaseCheese, escapePlayer};
	public AIMode activeMode = AIMode.attackPlayer;

	float AIDelay = 0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		target = player.transform;
		escapeSpot = GameObject.FindGameObjectWithTag ("EscapeSpot").transform;

		MyCheese.SetActive (false);

		Health.s.chaseCheese += CheckCheeseChase;
		Health.s.stopChase += StopChase;

		stoppingdist = agent.stoppingDistance;
		InvokeRepeating ("AgentUpdate", 0.1f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		if(activeMode == AIMode.attackPlayer)
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 5 * Time.deltaTime);
		if (AIDelay > 0f)
			AIDelay -= Time.deltaTime;
	}

	void AgentUpdate (){
		if (AIDelay > 0f) {
			agent.enabled = false;
		} else {
			agent.enabled = true;
			if (target == null) {
				if (activeMode == AIMode.chaseCheese) {
					activeMode = AIMode.attackPlayer;
					target = player;
					agent.stoppingDistance = stoppingdist;
				}
			}
			agent.SetDestination (target.position);
		}
	}


	public static float distance;
	public delegate void BasicDelegate(bool val);
	public static BasicDelegate BestOne;
	void CheckCheeseChase (){
		if (transform == null)
			return;

		float myDist = Vector3.Distance(transform.position, Health.s.activeCheese.transform.position);

		if (myDist < distance) {
			if (BestOne != null)
				BestOne (false);
			BestOne = ChaseCheese;
			BestOne (true);
		}

		distance = myDist;
	}

	void StopChase (){
		ChaseCheese (false);
	}

	float stoppingdist;
	void ChaseCheese (bool val){
		if (val) {
			AIDelay = 2f;
			//print (gameObject.name);
			target = Health.s.activeCheese.transform;
			activeMode = AIMode.chaseCheese;
			agent.stoppingDistance = 0;
		} else {
			//print ("s chase "+gameObject.name);
			target = player;
			activeMode = AIMode.attackPlayer;
			agent.stoppingDistance = stoppingdist;
		}
	}

	void OnDestroyed (){
		Health.s.chaseCheese -= CheckCheeseChase;
		Health.s.stopChase -= StopChase;

		DropCheese ();

		if (activeMode == AIMode.chaseCheese)
			Health.s.chaseCheese.Invoke ();
	}

	public GameObject Cheese;
	public GameObject MyCheese;

	public bool haveCheese = false;


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Cheese"))
		{
			AIDelay = 1f;
			Destroy(collision.gameObject);
			haveCheese = true;
			MyCheese.SetActive(true);
			activeMode = AIMode.escapePlayer;

			agent.speed = agent.speed / 2f;

			target = escapeSpot;
		}
	}

	public void DropCheese (){
		if (haveCheese) {
			GameObject aCheese = (GameObject)Instantiate (Cheese, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			Vector3 shootVector = Quaternion.Euler (0, Random.Range (0, 360), 0) * new Vector3 (150, 400, 0);
			aCheese.GetComponent<Rigidbody> ().AddForce (shootVector);

			haveCheese = false;

			activeMode = AIMode.attackPlayer;
			target = player;
			agent.stoppingDistance = stoppingdist;

			AIDelay = 2f;
		}
	}
}
