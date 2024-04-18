using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }

    [SerializeField]
    private Transform equipmentParent; // Parent object where equipped items will be stored (to keep the hierarchy organized)

    public Item[] currentEquipment; // Slots for the equipped items

    private Inventory inventory; // Reference to the player's inventory

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
        }

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
    }

    void Start()
    {
        inventory = Inventory.instance; // Assumes you have a singleton Inventory instance
    }

    public void Equip(Item newItem)
    {
        int slotIndex = (int)newItem.equipSlot; // Assume Item has an 'equipSlot' enum that corresponds to the type of item

        Item oldItem = null;

        // If there was already something in the slot, unequip it first
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        // Insert the item in the slot and use it
        currentEquipment[slotIndex] = newItem;
        AttachToEquipmentParent(newItem);
        Debug.Log(newItem.name + " equipped.");

        // Optionally, you can add an event or callback here to update the UI
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Item oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            RemoveFromEquipmentParent(oldItem);
            currentEquipment[slotIndex] = null;
            Debug.Log("Unequipped item from slot " + slotIndex);

            // Optionally, update the UI here
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if (currentEquipment[i] != null)
            {
                Unequip(i);
            }
        }
        Debug.Log("All items unequipped.");
    }

    private void AttachToEquipmentParent(Item item)
    {
        if (item.itemPrefab != null)
        {
            Instantiate(item.itemPrefab, equipmentParent);
        }
    }

    private void RemoveFromEquipmentParent(Item item)
    {
        foreach (Transform child in equipmentParent)
        {
            if (child.gameObject.name == item.itemPrefab.name + "(Clone)")
            {
                Destroy(child.gameObject);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) // Press U to unequip all
        {
            UnequipAll();
        }
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Accessory }
