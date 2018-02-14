using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;                            
    public int currentHealth;                                   
    public Slider healthSlider;                                 
    public Image damageImage;                                   
    public float flashSpeed = 5f;                               
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     

    PlayerMovement playerMovementScript;                       
    PlayerCombat playerShootingScript;                             
    bool isDead;                                                
    bool damaged;                                              

    public Transform RespawnPoint;

    void Awake () {
        // Setting up the references
        playerMovementScript = GetComponent <PlayerMovement> ();
        playerShootingScript = GetComponentInChildren <PlayerCombat> ();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }


    void Update () {
        if(damaged) {
            damageImage.color = flashColour;
        } else {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage (int amount) {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        healthSlider.value = currentHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if(currentHealth <= 0 && !isDead) {
            Death ();
        }
    }


    void Death ()
    {
        
        transform.position = RespawnPoint.position;
        startingHealth = 100;
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;

    }       
}
