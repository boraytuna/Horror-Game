using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float playerMaxHealth = 100f; 
    public float playerHealth; 

    // Declare the onDeath event
    public delegate void DeathAction();
    public event DeathAction onDeath;

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

        if (playerHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        playerHealth += amount; // Increase the player's health.
        playerHealth = Mathf.Min(playerHealth, playerMaxHealth); // Ensure health does not exceed maximum.
        Debug.Log("Player healed: " + amount + ". Current health: " + playerHealth);
    }

    void Die()
    {
        Debug.Log("Player has died!");

        // Trigger the death event
        if (onDeath != null)
        {
            onDeath();
        }

        // Here, you can handle the player's death.
        gameObject.SetActive(false);
    }
}
