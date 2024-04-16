using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    [SerializeField] public float maxBatteryLife = 600;  // Default value; can be changed in Unity Editor
    public float currentBatteryLife;
    public bool isActive = false;

    public Inventory inventory;  // Reference to the Inventory component

    void Start()
    {
        flashlight = GetComponent<Light>();
        currentBatteryLife = maxBatteryLife;
        flashlight.enabled = false; // Ensure the flashlight is off at the start
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) return; // Only operate if flashlight is active in hierarchy

        CheckToggleInput(); // Check for toggle input
        CheckRechargeInput();  // Check for recharge input

        if (isActive)
        {
            UpdateBatteryLife();
        }
    }

    // Method to check and handle recharge input
    private void CheckRechargeInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryRechargeFlashlight();
        }
    }

    private void TryRechargeFlashlight()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned to the flashlight.");
            return;
        }

        foreach (Item item in inventory.items)
        {
            if (item != null && item is Battery)
            {
                bool wasUsed = ((Battery)item).Use(gameObject); // Attempt to use the battery
                if (wasUsed)
                {
                    Debug.Log("Flashlight recharged from inventory.");
                    inventory.Remove(item); // Remove the battery from the inventory
                    break; // Stop after successfully using one battery
                }
            }
        }
    }

    public bool Recharge(float amount)
    {
        if (currentBatteryLife < maxBatteryLife)
        {
            currentBatteryLife += amount;
            if (currentBatteryLife > maxBatteryLife)
            {
                currentBatteryLife = maxBatteryLife;
            }
            Debug.Log($"Flashlight recharged. Current battery life: {currentBatteryLife}");
            return true; // Indicates the recharge was successful and the battery was used
        }
        Debug.Log("Flashlight is already fully charged.");
        return false; // Indicates no recharge was needed, battery not used
    }

    private void CheckToggleInput()
    {
        if (!InventoryUI.isUIActive && Input.GetMouseButtonDown(0))
        {
            ToggleFlashlight();
        }
    }

    private void ToggleFlashlight()
    {
        isActive = !isActive;
        flashlight.enabled = isActive;
    }

    private void UpdateBatteryLife()
    {
        if (currentBatteryLife > 0)
        {
            currentBatteryLife -= Time.deltaTime;
            flashlight.intensity = Mathf.Lerp(0, 1, currentBatteryLife / maxBatteryLife);
        }
        else
        {
            FlashLightDie();
        }
    }

    private void FlashLightDie()
    {
        flashlight.enabled = false;
        isActive = false;  // Ensure the flashlight can no longer be toggled on
        Debug.Log("Flashlight battery dead.");
    }
}
