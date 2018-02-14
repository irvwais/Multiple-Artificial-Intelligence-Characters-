using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	public float smoothing = 5f;
	Vector3 offset;


	// Use this for initialization
	void Start () {
		//Cursor.lockState = CursorLockMode.Locked;
		offset = transform.position - player.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		Vector3 playerCamPos = player.position + offset;
		transform.position = Vector3.Lerp (transform.position, playerCamPos, smoothing * Time.deltaTime);
	}
}
