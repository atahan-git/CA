using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;

	Transform target;

	enum AIMode {attackPlayer, chaseCheese, escapePlayer};
	AIMode activeMode = AIMode.attackPlayer;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		target = player.transform;

		Health.s.chaseCheese += CheckCheeseChase;
		Health.s.stopChase += StopChase;

		InvokeRepeating ("AgentUpdate", 0.1f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 5 * Time.deltaTime);
	}

	void AgentUpdate (){
		agent.SetDestination (target.position);
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

	void ChaseCheese (bool val){
		if (val) {
			target = Health.s.activeCheese.transform;
			activeMode = AIMode.chaseCheese;
		} else {
			target = player;
			activeMode = AIMode.attackPlayer;
		}
	}

}
