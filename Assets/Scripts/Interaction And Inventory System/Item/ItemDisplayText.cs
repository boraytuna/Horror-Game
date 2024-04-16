using UnityEngine;
using TMPro;

public class ItemDisplayText : MonoBehaviour
{
    public GameObject textMeshProPrefab;
    private GameObject textInstance;
    public Vector3 textOffset = new Vector3();
    public Transform playerTransform;
    public float visibilityRange; // The range within which the text is visible

    void Start()
    {
        if (textMeshProPrefab != null)
        {
            textInstance = Instantiate(textMeshProPrefab, transform.position + textOffset, Quaternion.identity, transform);
            textInstance.SetActive(false);
        }
        // Optionally find the player transform automatically if not set through other means
        if(playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // Make sure your player GameObject is tagged as "Player"
            if(player != null) 
            {
                playerTransform = player.transform;
            }
        }
    }

    void Update()
    {
        if(playerTransform != null && textInstance != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            bool shouldShowText = distance <= visibilityRange;
            UpdateTextVisibility(shouldShowText);
        }
    }

    public void UpdateTextVisibility(bool isVisible)
    {
        if (textInstance != null)
        {
            textInstance.SetActive(isVisible);
            if (isVisible)
            {
                // Calculate the direction vector from the text to the player, ensuring it's horizontal (y component is 0)
                Vector3 direction = playerTransform.position - textInstance.transform.position;
                direction.y = 0; // This ensures the rotation is only around the Y-axis, keeping the text upright.

                // Now, set the rotation of the text to face this direction. 
                textInstance.transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);

                textInstance.transform.position = transform.position + textOffset; // Update position
            }
        }
    }


}
