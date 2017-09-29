using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

	Transform player;
	NavMeshAgent agent;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		agent = GetComponent<NavMeshAgent> ();
		InvokeRepeating ("AgentUpdate", 0.1f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 5 * Time.deltaTime);
	}

	void AgentUpdate (){
		agent.SetDestination (player.position);
	}
}
