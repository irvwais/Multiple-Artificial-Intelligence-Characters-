using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

	public int startingHealth = 100;            
    public int currentHealth;                

    CapsuleCollider capsuleCollider;            
    bool isDead;                              


    void Awake () {
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth;
    }

    void Update () {

    }


    public void TakeDamage (int amount, Vector3 hitPoint) {

        if(isDead)
            return;

        currentHealth -= amount;
            
        if(currentHealth <= 0) {
            Death ();
        }
    }


    void Death () {
        // The enemy is dead.
        isDead = true;

        // Turn the collider into a trigger so shots can pass through it.
        capsuleCollider.isTrigger = true;
        GameManager.instance.AddKillCount(1);
        //GetComponent <NavMeshAgent> ().enabled = false;
        //GetComponent <Rigidbody> ().isKinematic = true;
        Destroy (gameObject);

    }
}
