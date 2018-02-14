using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

	GameObject target;
    PlayerHealth playerHealthScript;

    float speed = 10.0f;
    float maxAngularSpeed = 90.0f;

    Vector3 orientation = Vector3.up;

    // Use this for initialization
    void Awake () {
        target = GameObject.FindGameObjectWithTag("Player");
        playerHealthScript = target.GetComponent <PlayerHealth> ();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        Vector3 dir = Vector3.zero;
        dir = target.transform.position - transform.position;
        dir.Normalize();

        // Update orientation 
        orientation = dir;
        UpdateOrientation();
                
        Vector3 pos = transform.position;        
        Vector3 velocity = orientation * speed;
        pos += velocity * dt;

        transform.position = pos;

        Destroy (gameObject, 5f);
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
        Vector3 angle = new Vector3(0, 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        //Vector3 angle = new Vector3(-Mathf.Atan2(orientation.z, orientation.y), 0, -Mathf.Atan2(orientation.x, orientation.y) * Mathf.Rad2Deg);
        transform.eulerAngles = angle;
    }
}
