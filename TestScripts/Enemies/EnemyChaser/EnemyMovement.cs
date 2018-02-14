using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATES {
    WANDER,
    CHASE
}

public class EnemyMovement : MonoBehaviour {

	NavMeshAgent nav;
	Transform player;
	
	// Ranges
	float m_chaseRange = 30.0f;

	// States
    AI_STATES m_currentState;
    AI_STATES WanderState() { return AI_STATES.WANDER; }
    AI_STATES ChaseState() { return AI_STATES.CHASE; }

	// Wander
	float m_elapsedTime;
	float m_rotationDamping = 6.0f;
    float m_wanderRadius = 50.0f;
    float m_wanderTimer = 10.0f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		m_currentState = WanderState();
        GameManager.instance.enemySpawned++;
	}
	
	// Update is called once per frame
	void Update () {
		//nav.SetDestination (player.position);
		UpdateStates (); 
	}

	void UpdateStates () {

		float distance = Vector3.Distance(nav.transform.position, player.position);
        if(distance <= m_chaseRange) {
            m_currentState = ChaseState();
        } else {
            m_currentState = WanderState();
        }

        switch(m_currentState) {
            case AI_STATES.WANDER:
                if(m_elapsedTime < m_wanderTimer) {
                    m_elapsedTime += Time.deltaTime;
                } else {
                    Wander();
                    m_elapsedTime = 0;
                }
                break;
            case AI_STATES.CHASE:
                Chase();
				CheckRangeAndVisability ();
                // if(CheckRangeAndVisability()) {
                //     if(m_elapsedTime < m_shotCooldown) {
                //         m_elapsedTime += Time.deltaTime;
                //     } else {
                //         Shoot();
                //         m_elapsedTime = 0;
                //     }
                // }
                break;
        }
	}

	bool CheckRangeAndVisability() {
        bool isInRange = false;
        bool isVisible = false;
        bool chase = false;

        // if NPC is in range of the player
        float remainingDistance = Vector3.Distance(this.transform.position, player.transform.position);
        if (remainingDistance <= m_chaseRange) {
            isInRange = true;
            nav.updateRotation = false;
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            nav.transform.rotation = Quaternion.Slerp(nav.transform.rotation, rotation, Time.deltaTime * m_rotationDamping);            
        } else {
            nav.updateRotation = true;
        }

        if(isInRange && isVisible) {
            chase = true;
        }
        return chase;
    }

	 void Wander () {
        nav.updateRotation = true;
        Vector3 newPos = RandomNavSphere(nav.transform.position, m_wanderRadius, -1);
        nav.SetDestination(newPos);
    }

    void Chase () {
        nav.SetDestination(player.position);
    }

	Vector3 RandomNavSphere(Vector3 origin, float distance, int areaMask) {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, areaMask);
        return navHit.position;
    }
}
