using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;
    Inventory inventory;
    InventorySlot[] slots;
    
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        inventoryUI.SetActive(false);

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            bool isUIActive = !inventoryUI.activeSelf;

        // Toggle the active state of the UI panel
        inventoryUI.SetActive(isUIActive);

        if (isUIActive)
        {
            // If the UI is now active, unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            // If the UI is now inactive, lock the cursor and make it invisible
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        }
    }

    void UpdateUI()
    {
        Debug.Log("updating UI");
        for(int i =0; i < slots.Length; i++)
        {
            if(i <inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            } else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
