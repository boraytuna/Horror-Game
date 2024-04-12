using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public GameObject itemPrefab;
    public bool isDefaultItem = false;
    public bool showInInventory = true;

    // Adjusted to a single, flexible use method
    public abstract bool Use(GameObject user);
}