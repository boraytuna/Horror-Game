// using System.Collections.Generic;
// using UnityEngine;

// public class Inventory : MonoBehaviour
// {
//     public static Inventory instance; // Singleton instance

//     public List<Item> items = new List<Item>();

//     void Awake()
//     {
//         if (instance != null)
//         {
//             Debug.LogWarning("More than one instance of Inventory found!");
//             return;
//         }
//         instance = this; // Assign the static instance
//     }

//     public void Add(Item itemToAdd)
//     {
//         items.Add(itemToAdd);
//         Debug.Log($"Added {itemToAdd.itemName} to inventory. Total items: {items.Count}");
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 10;	// Amount of item spaces

	// Our current list of items in the inventory
	public List<Item> items = new List<Item>();

	// Add a new item if enough room
	public void Add (Item item)
	{
		if (item.showInInventory) {
			if (items.Count >= space) {
				Debug.Log ("Not enough room.");
				return;
			}

			items.Add (item);

			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke ();
		}
	}

	// Remove an item
	public void Remove (Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}

}