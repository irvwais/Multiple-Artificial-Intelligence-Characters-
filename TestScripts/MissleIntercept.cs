using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleIntercept : MonoBehaviour {

	GameObject target;
	//PlayerMovement playerMovementScript;
	PlayerHealth playerHealthScript;

    float speed = 10.0f;
    float maxAngularSpeed = 90.0f;
    Vector3 velocity = Vector3.zero;
    Vector3 orientation = Vector3.up;

	// Use this for initialization
	void Awake () {
	    target = GameObject.FindGameObjectWithTag("Player");
		//playerMovementScript = GetComponent<PlayerMovement>();	
        playerHealthScript = target.GetComponent <PlayerHealth> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float dt = Time.deltaTime;

        Vector3 targetVelocity = target.gameObject.GetComponent<Rigidbody>().velocity;
        Vector3 vr = targetVelocity - velocity;
        Vector3 sr = target.transform.position - transform.position;
        float tc = sr.magnitude / vr.magnitude;
        Vector3 st = target.transform.position + targetVelocity * tc;
                
        Vector3 dir = st - transform.position;
        dir.Normalize();

        
        float d = Vector3.Dot(dir, orientation.normalized);
        float a = Mathf.Acos(d) * Mathf.Rad2Deg;

        
        if (a > maxAngularSpeed * dt) {        
            bool rightDir = Vector3.Dot(new Vector3(orientation.y, -orientation.x, 0), dir) > 0;
            orientation = Quaternion.Euler(0, 0, maxAngularSpeed * dt * (rightDir ? -1 : 1)) * orientation;
        } else {
            orientation = dir;
        }
        UpdateOrientation();
        
        Vector3 pos = transform.position;
        velocity = orientation * speed;
        pos += velocity * dt;

        transform.position = pos;
    }

	void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag ("Player")) {
            playerHealthScript.TakeDamage (20);
            Destroy (gameObject);
        }

        if (other.gameObject.CompareTag ("Obsticale")) {
            Destroy(gameObject);
        }
    }

    void UpdateOrientation() {
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
