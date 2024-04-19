using UnityEngine;

public class ResourceManagement : MonoBehaviour
{
    public static ResourceManagement Instance { get; private set; }

    public float TotalStamina { get; private set; }
    public float TotalBatteryLife { get; private set; }

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

    public void ModifyStamina(float amount)
    {
        TotalStamina += amount;
        TotalStamina = Mathf.Clamp(TotalStamina, 0, 100); // Assuming 100 is max stamina
        Debug.Log("Stamina updated: " + TotalStamina);
    }

    public void ModifyBatteryLife(float amount)
    {
        TotalBatteryLife += amount;
        TotalBatteryLife = Mathf.Clamp(TotalBatteryLife, 0, 600); // Assuming 600 is max battery life
        Debug.Log("Battery life updated: " + TotalBatteryLife);
    }
}
