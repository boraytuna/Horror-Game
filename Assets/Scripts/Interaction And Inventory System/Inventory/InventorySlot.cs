using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        if (item != null)
        {
            // Call Inventory to drop the item
            Inventory.instance.DropItem(item);

            // Remove the item from the inventory
            Inventory.instance.Remove(item);

            // clear the slot visually
            ClearSlot();
            Debug.Log("Item removed and dropped.");
        }
    }

    public void onItemButtonUseItem()
    {
        if (item != null)
        {
            Debug.Log("Attempting to use " + item.name);

            GameObject user = GameObject.FindWithTag("Player");
            bool itemUsed = item.Use(user); // This now returns a bool indicating success
            
            if (itemUsed)
            {
                // If the item was successfully used, remove it from the inventory
                Inventory.instance.Remove(item);
            }
        }
    }

}