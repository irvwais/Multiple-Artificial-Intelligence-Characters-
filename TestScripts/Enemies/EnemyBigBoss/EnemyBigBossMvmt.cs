using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBigBossMvmt : MonoBehaviour {

	NavMeshAgent nav;
	Transform player;
	PlayerMovement playerMovementScript;
	public float bigBossMoveSpeed = 7.0f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		playerMovementScript = GetComponent<PlayerMovement>();
	}

	// Use this for initialization
	void Start () {
        //GameManager.instance.enemySpawned++;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.position, transform.position) < 50.0f)
			nav.SetDestination (player.position);

		 if (playerMovementScript.energyCoolDown == false && Vector3.Distance (player.position, transform.position) < 50.0f) {
			Debug.Log ("I am Chaising Faster");
            //Vector3 dir = transform.position - player.position;
            nav.SetDestination (player.position * bigBossMoveSpeed * Time.deltaTime);
        }
	}
}
