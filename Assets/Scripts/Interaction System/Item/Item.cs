using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    // Define a general use method that can be overridden by specific items
    public abstract void Use(GameObject user);
}
