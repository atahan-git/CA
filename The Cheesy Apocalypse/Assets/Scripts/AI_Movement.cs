using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Movement : MonoBehaviour {

	[HideInInspector]
	public Transform player;
	NavMeshAgent agent;

	Transform escapeSpot;

	Transform target;

	public enum AIMode {attackPlayer, chaseCheese, escapePlayer};
	public AIMode activeMode = AIMode.attackPlayer;

	float AIDelay = 0f;

    private void Awake()
    {
        haveCheese = false;
    }

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
		if (activeMode == AIMode.attackPlayer) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (player.position - transform.position), 5 * Time.deltaTime);
			GetComponent<AI_Shoot>().gunPos.rotation = Quaternion.Slerp (GetComponent<AI_Shoot>().gunPos.rotation, Quaternion.LookRotation ((player.position + Vector3.up*0.2f) - GetComponent<AI_Shoot>().gunPos.position), 20 * Time.deltaTime);
		}
		if (AIDelay > 0f)
			AIDelay -= Time.deltaTime;

		//print (activeMode);
	}

	void AgentUpdate (){
		if (AIDelay > 0f || (Vector3.Distance(transform.position,player.position) > 10f && activeMode == AIMode.attackPlayer)) {
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
		/*if (GetComponent<Transform>() == null)
			return;*/

		if (isDead)
			return;

		float myDist = Vector3.Distance(transform.position, Health.s.activeCheese.transform.position);

		if (myDist < distance) {
			if (BestOne != null)
				BestOne (false);
			BestOne = ChaseCheese;
			BestOne (true);
			distance = myDist;
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
			//print ("s chase "+gameObject.name);
			target = player;
			activeMode = AIMode.attackPlayer;
			agent.stoppingDistance = stoppingdist;
		}
	}

	bool isDead = false;
	void Die (){
		isDead = true;
		Health.s.chaseCheese -= CheckCheeseChase;
		Health.s.stopChase -= StopChase;

		DropCheese ();

		transform.position = new Vector3 (1000, 1000, 1000);

		if (BestOne == ChaseCheese)
			BestOne = null;

	}

	public GameObject Cheese;
	public GameObject MyCheese;

	[HideInInspector]
	public bool haveCheese = false;


	void OnCollisionEnter(Collision collision)
	{
		if (isDead)
			return;


		if (collision.gameObject.CompareTag("Cheese"))
		{
			print ("got cheese");
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
		if (haveCheese && MyCheese.activeInHierarchy == true) {
			GameObject aCheese = (GameObject)Instantiate (Cheese, new Vector3 (transform.position.x, transform.position.y, transform.position.z), transform.rotation);
			Vector3 shootVector = Quaternion.Euler (0, Random.Range (0, 360), 0) * new Vector3 (150, 400, 0);
            Health.s.activeCheese = aCheese;

            aCheese.GetComponent<Rigidbody> ().AddForce (shootVector);
			print ("dropped cheese" + aCheese);

			haveCheese = false;

			if (!isDead) {
				activeMode = AIMode.attackPlayer;
				target = player;
				agent.stoppingDistance = stoppingdist;
			}

			AIDelay = 2f;
		}
	}
}
