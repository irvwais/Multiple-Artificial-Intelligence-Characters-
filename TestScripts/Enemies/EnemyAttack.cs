using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;   
    public int attackDamage = 10;             

    GameObject player;                         
    PlayerHealth playerHealthScript;                
    EnemyHealth enemyHealthScript;                  
    bool playerInRange;                      
    float timer;                               


    void Awake () {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealthScript = player.GetComponent <PlayerHealth> ();
        enemyHealthScript = GetComponent<EnemyHealth>();
    }


    void OnTriggerEnter (Collider other) {
        if(other.gameObject == player) {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other) {
        if(other.gameObject == player) {
            playerInRange = false;
        }
    }


    void Update () {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealthScript.currentHealth > 0) {
            Attack ();
        }
    }


    void Attack () {
        timer = 0f;

        if(playerHealthScript.currentHealth > 0) {
            playerHealthScript.TakeDamage (attackDamage);
        }
    }
}
