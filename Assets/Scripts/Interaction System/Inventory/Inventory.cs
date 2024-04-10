using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Update()
    {
        // Example: Use the first item in the inventory when 'U' is pressed
        if (Input.GetKeyDown(KeyCode.U) && items.Count > 0)
        {
            UseItem(items[0]); // Use the first item for simplicity
        }
    }

    public void UseItem(Item item)
    {
        item.Use(gameObject); // Use the item, passing in the player as the user
        items.Remove(item); // Remove the item from inventory after use
    }

    public void Add(Item itemToAdd)
    {
        items.Add(itemToAdd);
        Debug.Log($"Added {itemToAdd.itemName} to inventory. Total items: {items.Count}");
    }
}
