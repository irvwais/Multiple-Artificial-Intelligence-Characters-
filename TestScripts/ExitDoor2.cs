using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor2 : MonoBehaviour {

	public GameObject ExitDoorTwo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			ExitDoorTwo.transform.position = Vector3.MoveTowards (ExitDoorTwo.transform.position,
																	new Vector3 (ExitDoorTwo.transform.position.x, -3f, ExitDoorTwo.transform.position.z), 
																	3 * Time.deltaTime);
		} 
	}
}
