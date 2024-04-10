using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    
    public void Interact(GameObject interactor)
    {
        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Add(item);
            Debug.Log($"Picked up {item.itemName}");
            Destroy(gameObject); // Destroy the pickup object
        }
    }
}