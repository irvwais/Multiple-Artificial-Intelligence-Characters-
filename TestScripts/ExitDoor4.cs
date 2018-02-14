using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor4 : MonoBehaviour {

	public GameObject ExitDoorFour;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			ExitDoorFour.transform.position = Vector3.MoveTowards (ExitDoorFour.transform.position,
																	new Vector3 (ExitDoorFour.transform.position.x, -3f, ExitDoorFour.transform.position.z), 
																	3 * Time.deltaTime);
		} 
	}
}
