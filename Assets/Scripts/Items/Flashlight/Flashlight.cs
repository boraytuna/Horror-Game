using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    [SerializeField] public float maxBatteryLife = 600; // Default value; can be changed in Unity Editor
    public float currentBatteryLife;
    public bool isActive = false;

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

        if (isActive)
        {
            UpdateBatteryLife();
        }
    }

    // Public method to recharge the flashlight
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
            return true;
        }
        Debug.Log("Flashlight is already fully charged.");
        return false;
    }

    // Method to check and handle toggle input
    private void CheckToggleInput()
    {
        // Check if the inventory UI is active; if it is, ignore mouse clicks
        if (!InventoryUI.isUIActive && Input.GetMouseButtonDown(0))
        {
            ToggleFlashlight();
        }
    }

    // Method to toggle the flashlight on or off
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
        isActive = false; // Ensure the flashlight can no longer be toggled on
        Debug.Log("Flashlight battery dead.");
    }
}
