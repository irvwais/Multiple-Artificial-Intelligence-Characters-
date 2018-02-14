using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_STATES_Rocket {
    WANDER,
    CHASE,
	SHOOT
}

public class EnemyMovmentRocket : MonoBehaviour {

	NavMeshAgent nav;
	Transform player;

	//Shooting
    [SerializeField] Transform m_shootPosition;
    [SerializeField] GameObject m_rocketPrefab;
    Missile missileScript;
    public float m_shootCooldown = 0.5f;
    public float m_rocketSpeed = 500.0f;
    Ray m_ray;
    RaycastHit m_hit;
	
	// Ranges
	float m_chaseRange = 30.0f;
	float m_shootRange = 30.0f;

	// States
    AI_STATES_Rocket m_currentState;
    AI_STATES_Rocket WanderState() { return AI_STATES_Rocket.WANDER; }
    AI_STATES_Rocket ChaseState() { return AI_STATES_Rocket.CHASE; }
	AI_STATES_Rocket ShootState() { return AI_STATES_Rocket.SHOOT; }
	// Wander
	float m_elapsedTime;
	float m_rotationDamping = 6.0f;
    float m_wanderRadius = 50.0f;
    float m_wanderTimer = 10.0f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = GetComponent<NavMeshAgent> ();
        missileScript = GetComponent<Missile>();
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
            case AI_STATES_Rocket.WANDER:
                if(m_elapsedTime < m_wanderTimer) {
                    m_elapsedTime += Time.deltaTime;
                } else {
                    Wander();
                    m_elapsedTime = 0;
                }
                break;
            case AI_STATES_Rocket.CHASE:
                Chase();
                if(CheckRangeAndVisability()) {
                    if(m_elapsedTime < m_shootCooldown) {
                        m_elapsedTime += Time.deltaTime;
                    } else {
                        Shoot();
                        m_elapsedTime = 0;
                    }
                }
                break;
        }
	}

	bool CheckRangeAndVisability() {
        bool isInRange = false;
        bool isVisible = false;
        bool shoot = false;

        // if NPC is in range of the player
        float remainingDistance = Vector3.Distance(this.transform.position, player.transform.position);
        if (remainingDistance <= m_chaseRange) {
            isInRange = true;
            nav.updateRotation = false;
            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);
            nav.transform.rotation = Quaternion.Slerp(nav.transform.rotation, rotation, Time.deltaTime * m_rotationDamping);
			m_ray = new Ray(m_shootPosition.position, m_shootPosition.forward);
            Debug.DrawRay(m_ray.origin, m_shootPosition.forward, Color.red);
            // shoot a ray to see if player is visible and nothing is blocking the shot view
            if (Physics.Raycast(m_ray, out m_hit, m_shootRange + 1)) {
                // if what was hit is a player
                if(m_hit.collider.CompareTag("Player")) {
                    isVisible = true;
                }
            }            
        } else {
            nav.updateRotation = true;
        }

        if(isInRange && isVisible) {
            shoot = true;
        }
        return shoot;
    }

	 void Wander() {
        nav.updateRotation = true;
        Vector3 newPos = RandomNavSphere(nav.transform.position, m_wanderRadius, -1);
        nav.SetDestination(newPos);
    }

    void Chase() {
        nav.SetDestination(player.position);
    }

	void Shoot() {
        GameObject rocket = Instantiate(m_rocketPrefab, m_shootPosition.position, Quaternion.identity);
        //rocket.GetComponent<Rigidbody>().AddForce(m_shootPosition.forward * m_rocketSpeed);
    }

	Vector3 RandomNavSphere(Vector3 origin, float distance, int areaMask) {
        Vector3 randDirection = Random.insideUnitSphere * distance;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, areaMask);
        return navHit.position;
    }
}
