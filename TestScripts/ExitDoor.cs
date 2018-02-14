using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

	public GameObject ExitDoor1;

	void Awake(){
		//Player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			ExitDoor1.transform.position = Vector3.MoveTowards (ExitDoor1.transform.position,
																	new Vector3 (ExitDoor1.transform.position.x, -3f, ExitDoor1.transform.position.z), 
																	3 * Time.deltaTime);
		} 
	}

	// void OnTriggerExit (Collider other) {
	// 	if (other.gameObject.CompareTag ("Player")) {
	// 		ExitDoor1.transform.position = Vector3.MoveTowards (ExitDoor1.transform.position,
	//  																new Vector3 (ExitDoor1.transform.position.x, 1.5f, ExitDoor1.transform.position.z), 
	//  																3 * Time.deltaTime);
	// 	}
	// }
}
