using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public bool isDefaultItem = false;
    public bool showInInventory = true;

    // Define a general use method that can be overridden by specific items
    public abstract void UseItem(GameObject user);
    public virtual void Use()
    {
        // Use the item
        Debug.Log("Using " + name);
    }
}
