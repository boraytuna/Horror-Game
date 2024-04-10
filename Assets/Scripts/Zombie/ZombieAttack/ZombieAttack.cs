using UnityEngine;

//This script manages the damage dealt by the zombie to the player
public class ZombieAttack : MonoBehaviour
{
    [Header("Zombie Attack")]
    public float zombieDamage;
    public float zombieDamageRate; // The rate in seconds at which damage is applied
    
    private float nextDamageTime = 0; // Tracks when the next attack can occur

    void Start()
    {
        nextDamageTime = Time.time; // Initialize nextDamageTime
    }

    // OnTriggerStay is called almost every frame while another collider is touching the trigger collider
    void OnTriggerStay(Collider other)
    {
        // Check if the collider belongs to the player and if enough time has passed to deal damage again
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(zombieDamage);
                nextDamageTime = Time.time + zombieDamageRate; // Set the next time the zombie can deal damage
                Debug.Log("Damage dealt at: " + Time.time + ". Next damage at: " + nextDamageTime);
            }
        }
    }
}
