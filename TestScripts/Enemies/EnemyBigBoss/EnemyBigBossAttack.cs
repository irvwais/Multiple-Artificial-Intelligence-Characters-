using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBigBossAttack : MonoBehaviour {

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
            AttackStrenght ();
            Attack ();
        }
    }


    void Attack () {
        timer = 0f;

        if(playerHealthScript.currentHealth > 0) {
            playerHealthScript.TakeDamage (attackDamage);
        }
    }

    void AttackStrenght () {
        if(playerHealthScript.currentHealth > 80) {
            GetComponent<Renderer>().material.color = Color.red;
			attackDamage = 30;
			timeBetweenAttacks = 2f;
           // playerHealthScript.TakeDamage (attackDamage);
        } else if(playerHealthScript.currentHealth > 50) {
            GetComponent<Renderer>().material.color = Color.yellow;
			attackDamage = 20;
			timeBetweenAttacks = 1.5f;
            //playerHealthScript.TakeDamage (attackDamage);
        } else if(playerHealthScript.currentHealth > 20) {
            GetComponent<Renderer>().material.color = Color.blue;
			attackDamage = 10;
			timeBetweenAttacks = 1f;
            //playerHealthScript.TakeDamage (attackDamage);
        } else{
            GetComponent<Renderer>().material.color = Color.green;
            attackDamage = 5;
			timeBetweenAttacks = 0.5f;
        }
    }
}
