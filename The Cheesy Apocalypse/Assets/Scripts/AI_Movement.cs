using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;

	Transform escapeSpot;

	Transform target;

	public enum AIMode {attackPlayer, chaseCheese, escapePlayer};
	public AIMode activeMode = AIMode.attackPlayer;

	public float AIDelay = 0f;

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
			agent.SetDestination (target.position);
		}
	}


	public static float distance;
	delegate void BasicDelegate(bool val);
	BasicDelegate BestOne;
	void CheckCheeseChase (){
		float myDist = Vector3.Distance(transform.position, Health.s.activeCheese.transform.position);

		if (myDist < distance) {
			if (BestOne != null)
				BestOne (false);
			BestOne = ChaseCheese;
			BestOne (true);
		}
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
			target = player;
			activeMode = AIMode.attackPlayer;
			agent.stoppingDistance = stoppingdist;
		}
	}

	void OnDestroyed (){
		Health.s.chaseCheese -= CheckCheeseChase;
		Health.s.stopChase -= StopChase;
	}
		
	public GameObject MyCheese;

	public bool haveCheese = true;


	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Cheese"))
		{
			AIDelay = 2f;
			Destroy(collision.gameObject);
			haveCheese = true;
			MyCheese.SetActive(true);
			activeMode = AIMode.escapePlayer;

			agent.speed = agent.speed / 2f;

			target = escapeSpot;
		}
	}
}
