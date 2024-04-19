using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerShoot playerShoot;
    [SerializeField] private Gun playerGun;
    [SerializeField] private KnifeAttack playerKnifeAttack;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private Flashlight playerFlashlight;

    public bool IsAlive { get; private set; } = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //SetupPlayerComponents();
    }

    // private void SetupPlayerComponents()
    // {
    //     playerHealth = GetComponent<PlayerHealth>();
    //     playerMovement = GetComponent<PlayerMovement>();
    //     playerShoot = GetComponent<PlayerShoot>();
    //     playerGun = GetComponent<Gun>();
    //     playerKnifeAttack = GetComponent<KnifeAttack>();
    //     playerInventory = GetComponent<Inventory>();
    //     playerFlashlight = GetComponent<Flashlight>();

    //     // Subscribe to player health changes for UI update
    //     if (playerHealth != null)
    //     {
    //         playerHealth.onHealthChanged += UIManager.Instance.UpdateHealthUI;
    //     }

    //     // Other initializations or error checks can be added here
    // }

    // private void HandlePlayerDeath()
    // {
    //     IsAlive = false;
    //     DisablePlayerControls();
    //     GameManager.Instance.PlayerDied();
    //     Debug.Log("Player has died.");
    // }

    // private void DisablePlayerControls()
    // {
    //     playerMovement.enabled = false;
    //     playerShoot.enabled = false;
    //     if (playerGun != null) playerGun.enabled = false;
    //     if (playerKnifeAttack != null) playerKnifeAttack.enabled = false;
    // }

    // public void HealPlayer(float amount)
    // {
    //     if (playerHealth != null)
    //     {
    //         playerHealth.Heal(amount);
    //         UIManager.Instance.UpdateHealthUI(playerHealth.CurrentHealth);
    //     }
    // }

    // public void DamagePlayer(float damage)
    // {
    //     if (playerHealth != null)
    //     {
    //         playerHealth.TakeDamage(damage);
    //         UIManager.Instance.UpdateHealthUI(playerHealth.CurrentHealth);
    //     }
    // }

    // void Update()
    // {
    //     if (!IsAlive) return;

    //     // Update flashlight UI if needed
    //     if (playerFlashlight != null)
    //     {
    //         UIManager.Instance.UpdateBatteryUI(playerFlashlight.CurrentBatteryLife);
    //     }
    // }
}
