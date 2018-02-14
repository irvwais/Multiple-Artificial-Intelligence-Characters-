using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySentryMovement : MonoBehaviour {

	// Sentry moving speed
    public float speed = 10f;
    
    NavMeshAgent agent;

    // waypoints to patrol
    public List<Vector3> waypoints;
    int curWaypointIndex = -1;

    public enum State { PATROL, CHASE, TRACK } // for FSM
    State state;

    // target object
    GameObject player;

    // last position where the player was seen
    Vector3 lastPlayerPos;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
        state = State.PATROL; // Initial state
		player = GameObject.FindGameObjectWithTag ("Player");
    }

    Vector3 GetNextWaypoint()
    {
        if (waypoints.Count < 2)
            return transform.position;

        curWaypointIndex++;
        if (curWaypointIndex >= waypoints.Count)
        {
            curWaypointIndex = 0;
        }

        return waypoints[curWaypointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.PATROL:                
                {
                    GetComponent<Renderer>().material.color = Color.green;

                    Vector3 dir = player.transform.position - transform.position;
                                            
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, dir.normalized, out hit))
                    {
                        if (hit.collider.gameObject == player)
                        {
                            state = State.CHASE;

                            agent.SetDestination(player.transform.position);
                            
                            lastPlayerPos = player.transform.position;                 
                            break;
                        }
                    }
                    
                    float distToWaypoint = agent.remainingDistance;
                    if (Mathf.Approximately(distToWaypoint, 0))
                    {                        
                        agent.SetDestination(GetNextWaypoint());
                    }
                }
                break;
            case State.CHASE:
                {
                    GetComponent<Renderer>().material.color = Color.red;
                    Debug.DrawLine(transform.position, player.transform.position,Color.red);

                    Vector3 dir = player.transform.position - transform.position;
                                                            
                    RaycastHit hit;                    
                    if (Physics.Raycast(transform.position, dir.normalized, out hit))
                    {
                        if (hit.collider.gameObject == player)
                        {
                            state = State.CHASE;
                            agent.SetDestination(player.transform.position);
                            lastPlayerPos = player.transform.position;
                            break;
                        }
                    }
                
                    state = State.TRACK;
                    agent.SetDestination(lastPlayerPos);
                }
                break;
            case State.TRACK:                
                {
                    GetComponent<Renderer>().material.color = Color.yellow;

                    Vector3 dir = player.transform.position - transform.position;
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, dir.normalized, out hit))
                    {
                        if (hit.collider.gameObject == player)
                        {
                            state = State.CHASE;
                            agent.SetDestination(player.transform.position);
                            lastPlayerPos = player.transform.position;
                    
                            break;
                        }
                    }
                                        
                    float distToTarget = agent.remainingDistance;
                    if (Mathf.Approximately(distToTarget, 0))
                    {
                        state = State.PATROL;
                        agent.SetDestination(GetNextWaypoint());
                        break;
                    }

                }
                break;
        }
    }
}
