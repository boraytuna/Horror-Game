using UnityEngine;

[CreateAssetMenu(fileName = "RunBoosterItem", menuName = "Inventory/Item/PlayerBoost/RunBoosterItem")]
public class RunBoosterItem : Item
{
    [Range(10, 50)]
    public int BoostAmount;

    public override bool Use(GameObject user)
    {
        PlayerMovement playerMovement = user.GetComponent<PlayerMovement>();
        if (playerMovement != null && playerMovement.stamina < playerMovement.maxStamina)
        {
            playerMovement.BoostStamina(BoostAmount);
            Debug.Log($"{itemName} used, stamina boosted by {BoostAmount}.");
            return true; // Stamina boost applied, item was used
        }
        Debug.Log("Stamina boost item was not used.");
        return false; // Boost not applied, item was not used
    }
}
