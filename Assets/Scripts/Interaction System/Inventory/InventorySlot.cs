// using UnityEngine;
// using UnityEngine.UI;

// public class InventorySlot : MonoBehaviour
// {
//     public Image icon;
//     public Button removeButton;
//     Item item;

//     public void AddItem(Item newItem)
//     {
//         item = newItem;

//         icon.sprite = item.icon;
//         icon.enabled = true;
//         removeButton.interactable = true;
//     }

//     public void ClearSlot()
//     {
//         item = null;
//         icon.sprite = null;
//         icon.enabled = false;
//         removeButton.interactable = false;
//     }

//     public void OnRemoveButton()
//     {
//         if (item != null)
//         {
//             // Directly remove this specific item instance
//             Inventory.instance.Remove(item);
//             ClearSlot(); // Make sure this updates the UI accordingly

//             Debug.Log("Item removed and dropped.");
//         }
//     }


//     public void onItemButtonUseItem()
//     {
//         if (item != null)
//         {
//             Debug.Log("Attempting to use " + item.name);

//             GameObject user = GameObject.FindWithTag("Player");
//             bool itemUsed = item.Use(user); // This now returns a bool indicating success
            
//             if (itemUsed)
//             {
//                 // If the item was successfully used, remove it from the inventory
//                 Inventory.instance.Remove(item);
//             }
//         }
//     }

// }
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

            // Optionally clear the slot visually, if not automatically handled elsewhere
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