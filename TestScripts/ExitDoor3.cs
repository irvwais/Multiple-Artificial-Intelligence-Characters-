using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor3 : MonoBehaviour {

	public GameObject ExitDoorThree;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			ExitDoorThree.transform.position = Vector3.MoveTowards (ExitDoorThree.transform.position,
																	new Vector3 (ExitDoorThree.transform.position.x, -3f, ExitDoorThree.transform.position.z), 
																	3 * Time.deltaTime);
		} 
	}
}
