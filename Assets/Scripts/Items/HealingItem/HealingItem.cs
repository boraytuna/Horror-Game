using UnityEngine;

[CreateAssetMenu(fileName = "NewHealingItem", menuName = "Inventory/Item/PlayerBoost/Healing Item")]
public class HealingItem : Item
{
    [Range(10, 50)]
    public int healAmount;

    public override bool Use(GameObject user)
    {
        PlayerHealth playerHealth = user.GetComponent<PlayerHealth>();
        if (playerHealth != null && playerHealth.playerHealth < playerHealth.playerMaxHealth)
        {
            playerHealth.Heal(healAmount);
            Debug.Log($"{itemName} used, healing for {healAmount}.");
            return true; // Heal applied, item was used
        }
        Debug.Log("Healing item was not used.");
        return false; // Heal not applied, item was not used
    }

}
