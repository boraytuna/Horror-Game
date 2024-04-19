using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }
    
    public Inventory inventory;  // This assumes your inventory is a separate component
    public int MaxSpace = 20;  // Example max space, adjustable via inspector

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

    void Start()
    {
        inventory = GetComponent<Inventory>();
        if (inventory == null)
        {
            Debug.LogError("Inventory component is not attached to " + gameObject.name);
            return; // Skip further execution to prevent the null reference exception
        }

        inventory.space = MaxSpace;
        inventory.onItemChangedCallback += UpdateUI;
    }

    // Update UI method that could be called when items are added or removed
    private void UpdateUI()
    {
        // Update the inventory UI here
        Debug.Log("Inventory updated. Total items: " + inventory.items.Count);
    }

    public bool AddItem(Item item)
    {
        if (inventory.items.Count >= MaxSpace)
        {
            Debug.Log("Not enough room to add item.");
            return false;
        }

        inventory.items.Add(item);
        inventory.onItemChangedCallback.Invoke();
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (inventory.items.Remove(item))
        {
            inventory.onItemChangedCallback.Invoke();
        }
    }

    public void UseItem(Item item, GameObject user)
    {
        if (item.Use(user))
        {
            RemoveItem(item);
        }
    }
}
