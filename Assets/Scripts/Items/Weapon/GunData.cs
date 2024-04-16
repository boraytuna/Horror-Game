using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Inventory/Item/Weapon/Gun")]
public class GunData : Item
{
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("Reloading")]
    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool reloading;

    [Header("Ammo")]
    public AmmoData compatibleAmmo;     

    public override bool Use(GameObject user)
    {
        Gun gun = user.GetComponent<Gun>();
        if (gun != null && !reloading)
        {
            gun.StartReload();
            return true; // Reload started
        }
        return false; // Reload not started
    }

    //Reload the gun directly from the Inventory
    public void ReloadFromInventory(Inventory inventory)
    {
        // Find the latest compatible ammo in the inventory
        for (int i = inventory.items.Count - 1; i >= 0; i--)
        {
            if (inventory.items[i] is AmmoData ammoItem && ammoItem == compatibleAmmo)
            {
                if (currentAmmo < magSize && !reloading)
                {
                    int neededAmmo = magSize - currentAmmo;
                    int ammoToLoad = Mathf.Min(neededAmmo, ammoItem.bulletsPerBox);

                    currentAmmo += ammoToLoad;
                    ammoItem.bulletsPerBox -= ammoToLoad;

                    if (ammoItem.bulletsPerBox <= 0)
                    {
                        inventory.Remove(ammoItem);  // Remove the ammo item from inventory
                    }

                    inventory.onItemChangedCallback?.Invoke();  // Update the inventory UI
                    Debug.Log("Gun reloaded using inventory ammo.");
                    return;
                }
            }
        }
    }

}
