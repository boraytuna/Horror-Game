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
	

	public int space;	// Amount of item spaces

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

	public void Remove(Item item)
	{
		int index = items.IndexOf(item);
		if (index != -1)
		{
			items.RemoveAt(index);
			Debug.Log($"Removed item at index {index}. Total items now: {items.Count}");
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
	}


	// Method to drop the item into the world
	public void DropItem(Item itemToDrop)
    {
        if (itemToDrop != null && itemToDrop.itemPrefab != null)
        {
            // Get the camera transform from the FirstPersonCamera attached to the main camera or player
            Transform cameraTransform = Camera.main.transform;  // Assumes the main camera has the FirstPersonCamera script

            // Calculate drop position in front of the camera
            float dropDistance = 1.5f; // Distance in front of the camera to drop the item
            Vector3 dropPosition = cameraTransform.position + cameraTransform.forward * dropDistance;

            // Instantiate the item prefab at the calculated position
            GameObject droppedItem = Instantiate(itemToDrop.itemPrefab, dropPosition, Quaternion.identity);

            // Optionally add force or additional effects
            Rigidbody itemRb = droppedItem.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                float throwForce = 2f;  // Customize as necessary
                itemRb.AddForce(cameraTransform.forward * throwForce, ForceMode.VelocityChange);
            }

            Debug.Log($"{itemToDrop.name} dropped in front of the player.");
        }
        else
        {
            Debug.Log("Item prefab is missing or not assigned.");
        }
    }


}