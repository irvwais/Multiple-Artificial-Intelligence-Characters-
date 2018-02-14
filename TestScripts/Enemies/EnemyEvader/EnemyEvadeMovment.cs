using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEvadeMovment : MonoBehaviour {

	NavMeshAgent nav;
	Transform player;
	EnemyHealth enemyHealthScript;
	public float evadeSpeed = 7.0f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		enemyHealthScript = GetComponent<EnemyHealth>();
	}

	// Use this for initialization
	void Start () {
        GameManager.instance.enemySpawned++;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealthScript.currentHealth > 40 && Vector3.Distance (player.position, transform.position) < 100.0f)
			nav.SetDestination (player.position);

		if (enemyHealthScript.currentHealth <= 40) {
			//Debug.Log ("EnemyEvade is Running Away!");
			Vector3 dir = transform.position - player.position;
			nav.Move (dir.normalized * evadeSpeed * Time.deltaTime);
		}
	}
}
