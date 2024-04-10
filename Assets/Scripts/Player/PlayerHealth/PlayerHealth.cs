using UnityEngine;

//This script manages the player health such as getting damaged, healing and dying
public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float playerMaxHealth = 100f; 
    public float playerHealth; 

    void Start()
    {
        playerHealth = playerMaxHealth; 
    }

    void Update()
    {
        if (playerHealth <= 0)
        {
            Die(); // Call the die function when health is 0 or less.
        }
    }

    public void TakeDamage(float amount)
    {
        playerHealth -= amount; // Reduce the player's health by the damage amount.
        Debug.Log("Player took damage: " + amount + ". Current health: " + playerHealth);

        // Optionally, update health UI or invoke other effects here.
    }

    public void Heal(float amount)
    {
        playerHealth += amount; // Increase the player's health.
        playerHealth = Mathf.Min(playerHealth, playerMaxHealth); // Ensure health does not exceed maximum.
        Debug.Log("Player healed: " + amount + ". Current health: " + playerHealth);

        // Optionally, update health UI or invoke other healing effects here.
    }

    void Die()
    {
        Debug.Log("Player has died!");

        // Here, you can handle the player's death.
        // This could include playing a death animation, restarting the level,
        // showing a game over screen, or other actions.

        // This example simply deactivates the player object.
        gameObject.SetActive(false);
    }
}
