using UnityEngine;

[CreateAssetMenu(fileName = "Ammo", menuName = "Inventory/Item/Weapon/Ammo")]
public class AmmoData : Item
{
    public int bulletsPerBox;

    public override bool Use(GameObject user)
    {
        // Get the Inventory component from the user
        Inventory inventory = user.GetComponent<Inventory>();
        if (inventory == null)
        {
            return false; // No inventory found, cannot use the ammo
        }

        // Assuming the gun is a child of the user
        Gun gun = user.GetComponentInChildren<Gun>();
        if (gun == null)
        {
            return false; // No gun found, cannot use the ammo
        }

        // Check if the ammo matches the gun's compatible ammo and the magazine is not full
        if (this == gun.gunData.compatibleAmmo && gun.gunData.currentAmmo < gun.gunData.magSize)
        {
            gun.gunData.ReloadFromInventory(inventory);
            return true; // Ammo was used to reload the gun
        }

        // Ammo is compatible but the magazine is full or ammo does not match
        if (gun.gunData.currentAmmo == gun.gunData.magSize)
        {
            Debug.Log("Magazine is already full. Cannot use ammo.");
        }
        else
        {
            Debug.Log("Incompatible ammo.");
        }

        return false; // Ammo could not be used
    }
}
