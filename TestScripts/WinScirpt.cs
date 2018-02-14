using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScirpt : MonoBehaviour {

	public Text winText;

	// Use this for initialization
	void Start () {
		winText.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			winText.enabled = true;
			Time.timeScale = 0.0f;
		}
	}
}
