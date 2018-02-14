using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {

	public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;                      

    float timer;                                   
    Ray shootRay;                                  
    RaycastHit shootHit;                           
    int shootableMask;                             
	public LineRenderer gunLine;                          

    float effectsDisplayTime = 0.2f;                

    void Awake () {
        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask ("Shootable");
		//gunLine = GetComponent <LineRenderer> ();
    }

    void Update () {
        timer += Time.deltaTime;

        if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets) {
            Shoot ();
        }
    }


    void Shoot () {
        // Reset the timer.
        timer = 0f;

		// Enable the line renderer and set it's first position to be the end of the gun.
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        // Set the shootRay so that it starts at the end of the gun and points forward from the barrel.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // Perform the raycast against gameobjects on the shootable layer and if it hits something...
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) {
            // Try and find an EnemyHealth script on the gameobject hit.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();

            if(enemyHealth != null) {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }

			// Set the second position of the line renderer to the point the raycast hit.
            gunLine.SetPosition (1, shootHit.point);
        }
        // If the raycast didn't hit anything on the shootable layer
        else {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
