using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public Item item;
    public GameObject textMeshProPrefab; 
    public Vector3 textOffset = new Vector3(); // Adjustable offset in the Inspector
    private GameObject promptInstance;
    private Transform playerTransform;

    private void Start()
    {
        if (textMeshProPrefab != null)
        {
            promptInstance = Instantiate(textMeshProPrefab, transform.position + textOffset, Quaternion.identity, transform);
            promptInstance.SetActive(false); // Start with the prompt disabled
        }
    }

    private void Update()
    {
        if (promptInstance.activeSelf)
        {
            // Ensure the text always faces the player
            promptInstance.transform.rotation = Quaternion.LookRotation(promptInstance.transform.position - playerTransform.position);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerTransform == null)
            {
                playerTransform = other.transform; // Cache the player's transform
            }

            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance <= 3f)
            {
                promptInstance.SetActive(true);
                promptInstance.transform.position = transform.position + textOffset; // Update position in case of changes
            }
            else
            {
                promptInstance.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            promptInstance.SetActive(false);
        }
    }

    public void Interact(GameObject interactor)
    {
        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory != null)
        {
            inventory.Add(item);
            Debug.Log($"Picked up {item.itemName}");
            Destroy(gameObject); // Destroy the pickup object
        }
    }
}
