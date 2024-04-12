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

	// Method to drop the item into the world
		public void DropItem(Item itemToDrop)
	{
		if (itemToDrop != null && itemToDrop.itemPrefab != null)
		{
			// Get the player camera from the PlayerCamera script (assuming it's attached to the camera object)
			Transform cameraTransform = PlayerCamera.instance.transform;

			// Calculate drop position in front of the camera
			float dropDistance = 1.0f; 
			Vector3 dropPosition = cameraTransform.position + cameraTransform.forward * dropDistance + cameraTransform.up * -0.5f; // Slightly below the camera center

			// Instantiate the item prefab at the calculated position
			GameObject droppedItem = Instantiate(itemToDrop.itemPrefab, dropPosition, Quaternion.identity);

			// Optionally add force or additional effects
			Rigidbody itemRb = droppedItem.GetComponent<Rigidbody>();
			if (itemRb != null)
			{
				itemRb.AddForce(cameraTransform.forward * 5f, ForceMode.VelocityChange); // Adds a forward force to the item
			}

			Debug.Log($"{itemToDrop.name} dropped in front of the player.");
		}
		else
		{
			Debug.Log("Item prefab is missing or not assigned.");
		}
	}


}