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
        }

        SetupPlayerComponents();
    }

    private void SetupPlayerComponents()
    {
        // Attempt to get the PlayerHealth component and log an error if it is not found
        playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth == null)
            Debug.LogError("PlayerHealth component not found on " + gameObject.name);

        // Attempt to get the PlayerMovement component and log an error if it is not found
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null)
            Debug.LogError("PlayerMovement component not found on " + gameObject.name);

        // Attempt to get the PlayerShoot component and log an error if it is not found
        playerShoot = GetComponent<PlayerShoot>();
        if (playerShoot == null)
            Debug.LogError("PlayerShoot component not found on " + gameObject.name);

        // Attempt to get the Gun component and log an error if it is not found
        playerGun = GetComponent<Gun>();
        if (playerGun == null)
            Debug.LogError("Gun component not found on " + gameObject.name);

        // Attempt to get the KnifeAttack component and log an error if it is not found
        playerKnifeAttack = GetComponent<KnifeAttack>();
        if (playerKnifeAttack == null)
            Debug.LogError("KnifeAttack component not found on " + gameObject.name);

        // Attempt to get the Inventory component and log an error if it is not found
        playerInventory = GetComponent<Inventory>();
        if (playerInventory == null)
            Debug.LogError("Inventory component not found on " + gameObject.name);

        // Attempt to get the Flashlight component and log an error if it is not found
        playerFlashlight = GetComponent<Flashlight>();
        if (playerFlashlight == null)
            Debug.LogError("Flashlight component not found on " + gameObject.name);

        // If any component is missing, return early to avoid further null references
        if (playerHealth == null || playerMovement == null || playerShoot == null ||
            playerGun == null || playerKnifeAttack == null || playerInventory == null ||
            playerFlashlight == null)
        {
            Debug.LogError("SetupPlayerComponents failed: One or more components are missing.");
            return;
        }

        // Subscribe to the playerHealth's death event
        playerHealth.onDeath += HandlePlayerDeath;
    }

    private void HandlePlayerDeath()
    {
        IsAlive = false;
        DisablePlayerControls();
        GameManager.Instance.PlayerDied();
        Debug.Log("Player has died.");
    }

    private void DisablePlayerControls()
    {
        playerMovement.enabled = false;
        playerShoot.enabled = false;
        if (playerGun != null) playerGun.enabled = false;
        if (playerKnifeAttack != null) playerKnifeAttack.enabled = false;
    }

    public void HealPlayer(float amount)
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(amount);
        }
    }

    public void DamagePlayer(float damage)
    {
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    void Update()
    {
        if (!IsAlive) return;

    }
}
