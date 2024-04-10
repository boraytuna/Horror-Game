using UnityEngine;

[CreateAssetMenu(fileName = "NewHealingItem", menuName = "Inventory/Item/Healing Item")]
public class HealingItem : Item
{
    [Range(10, 50)]
    public int healAmount;

    public override void Use(GameObject user)
    {
        PlayerHealth playerHealth = user.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);
            Debug.Log($"{itemName} used, healing for {healAmount}.");
        }
    }
}
