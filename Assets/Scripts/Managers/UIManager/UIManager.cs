using UnityEngine;
using UnityEngine.UI;
using TMPro; // For text handling

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    // UI elements references
    [Header("Player Stats UI")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI bulletText;
    public TextMeshProUGUI batteryText;

    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    public Transform inventorySlotsParent;
    InventorySlot[] inventorySlots;

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
    }

    void Start()
    {
        // Initialize Inventory UI
        Inventory.instance.onItemChangedCallback += UpdateInventoryUI;

        // Prepare inventory slots
        inventorySlots = inventorySlotsParent.GetComponentsInChildren<InventorySlot>();
        UpdateInventoryUI();
    }

    public void UpdateHealthUI(int currentHealth)
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }

    public void UpdateStaminaUI(float currentStamina)
    {
        staminaText.text = "Stamina: " + Mathf.RoundToInt(currentStamina).ToString();
    }

    public void UpdateBulletUI(int currentBullets)
    {
        bulletText.text = "Bullets: " + currentBullets.ToString();
    }

    public void UpdateBatteryUI(float currentBattery)
    {
        batteryText.text = "Battery: " + Mathf.RoundToInt(currentBattery).ToString();
    }

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
            {
                inventorySlots[i].AddItem(Inventory.instance.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }

    // Function to toggle inventory UI visibility
    public void ToggleInventoryUI()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }

    // Call these methods from respective managers or directly from the component scripts
    public void OnPlayerHealthChanged(int currentHealth)
    {
        UpdateHealthUI(currentHealth);
    }

    // Call this method when player stamina changes
    public void OnPlayerStaminaChanged(float currentStamina)
    {
        UpdateStaminaUI(currentStamina);
    }

    // Update bullet count when ammo is used
    public void OnPlayerAmmoChanged(int currentAmmo)
    {
        UpdateBulletUI(currentAmmo);
    }

    // Update battery life from Flashlight script
    public void OnBatteryLifeChanged(float currentBatteryLife)
    {
        UpdateBatteryUI(currentBatteryLife);
    }
}
