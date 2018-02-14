using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public static PlayerMovement instance;
	public float speed;
	public float dodgeSpeed = 100f;
	//public float runSpeed = 10f;
	public float startingEnergy = 100f;
	public float currentEnergy;
	public Slider energySlider;
	public bool energyCoolDown = false;
	float energyCoolDownTimer = 3f;
	Vector3 movement; 
	Vector3 dodge;
	//Vector3 run;
	Rigidbody rb;
	int floorMask;
	float camRayLength = 100f;

	void Awake() {
		instance = this; 
		
		floorMask = LayerMask.GetMask ("Floor");
		rb = GetComponent<Rigidbody> ();

		currentEnergy = startingEnergy;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (energyCoolDown) {
			energyCoolDownTimer -= Time.deltaTime;
			if (energyCoolDownTimer <= 0.0f) {
				currentEnergy += 1f;
		 		currentEnergy = Mathf.Clamp (currentEnergy, 0, startingEnergy);
		 		energySlider.value = currentEnergy;
			}
		}
	}

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		Walk (h, v);
		Turning ();
		speed = 5f;

		if (Input.GetButtonDown ("Jump")) {
			Dodge (h, v);
			currentEnergy -= 5f;
			currentEnergy = Mathf.Clamp (currentEnergy, 0, startingEnergy);
			energySlider.value = currentEnergy;
			energyCoolDown = false;
			energyCoolDownTimer = 3f;

		} 
		
		if (Input.GetKey (KeyCode.LeftShift) && currentEnergy > 0) {
			speed = 15f;
			currentEnergy -= .25f;
			currentEnergy = Mathf.Clamp (currentEnergy, 0, startingEnergy);
			energySlider.value = currentEnergy; 
			energyCoolDown = false;
			energyCoolDownTimer = 3f;
		}  
		
		if (Input.GetKeyUp (KeyCode.LeftShift) || Input.GetButtonUp ("Jump")){
			energyCoolDown = true;
			//StartCoroutine (EnergyRefill());
		}

		if (currentEnergy == 0) {
			Walk (h, v);
		}
	}

	// IEnumerator EnergyRefill () {
	// 	while (true) {
	// 		if (currentEnergy < startingEnergy && energyCoolDown) {
	// 			yield return new WaitForSeconds (3f);
	// 			currentEnergy += 1f;
	// 	 		currentEnergy = Mathf.Clamp (currentEnergy, 0, startingEnergy);
	// 	 		energySlider.value = currentEnergy;
	// 		} else {
	// 			yield return null;
	// 		}
	// 	}
	// }

	void Dodge (float right, float up) {
		dodge.Set (right, 0f, up);
		dodge = dodge.normalized * dodgeSpeed * Time.deltaTime;
		rb.MovePosition (transform.position + dodge);
	}

	void Walk (float h, float v) {
		movement.Set (h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		rb.MovePosition (transform.position + movement);
	}

	void Turning () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask)) {
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			rb.MoveRotation (newRotation);
		}
	}

	public Vector3 GetVelocity () {
		return movement;
	}
}
