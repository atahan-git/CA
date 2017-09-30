using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;

	Transform target;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		target = player.transform;

		Health.s.chaseCheese += CheckCheeseChase;

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
			BestOne (false);
			BestOne = BestOneCallBack;
			BestOne (true);
		}
	}

	void BestOneCallBack (bool isStillBest){
		if (isStillBest)
			target = Health.s.activeCheese.transform;
		else
			target = player;
	}

}
