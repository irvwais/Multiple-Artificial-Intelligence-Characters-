using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_BIG_BOSS_STATES {
    WANDER,
    CHASE
}
public class EnemyBigBossMovement : MonoBehaviour {

	NavMeshAgent nav;
	Transform player;
	//PlayerMovement playerMovementScript;
	public float bigBossMoveSpeed = 10.0f;             

	// Ranges
	float m_chaseRange = 30.0f;

	// States
    AI_BIG_BOSS_STATES m_currentState;
    AI_BIG_BOSS_STATES WanderState() { return AI_BIG_BOSS_STATES.WANDER; }
    AI_BIG_BOSS_STATES ChaseState() { return AI_BIG_BOSS_STATES.CHASE; }

	// Wander
	float m_elapsedTime;
	float m_rotationDamping = 6.0f;
    float m_wanderRadius = 50.0f;
    float m_wanderTimer = 10.0f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
		//playerMovementScript = GetComponent<PlayerMovement> ();
	}

	// Use this for initialization
	void Start () {
		m_currentState = WanderState();
        //GameManager.instance.enemySpawned++;
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
            case AI_BIG_BOSS_STATES.WANDER:
                if(m_elapsedTime < m_wanderTimer) {
                    m_elapsedTime += Time.deltaTime;
                } else {
                    Wander();
                    m_elapsedTime = 0;
                }
                break;
            case AI_BIG_BOSS_STATES.CHASE:
                Chase();
				CheckRangeAndVisability ();
                // if(CheckRangeAndVisability()) {
				// 	if (!playerMovementScript.energyCoolDown){
                //         Vector3 dir = transform.position - player.position;
                //         nav.Move (dir.normalized * bigBossMoveSpeed * Time.deltaTime);
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
        if (!PlayerMovement.instance.energyCoolDown){
            Vector3 dir = transform.position - player.position;
            nav.SetDestination(player.position * bigBossMoveSpeed * Time.deltaTime);
        }
    }

	Vector3 RandomNavSphere(Vector3 origin, float distance, int areaMask) {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, areaMask);
        return navHit.position;
    }
}
