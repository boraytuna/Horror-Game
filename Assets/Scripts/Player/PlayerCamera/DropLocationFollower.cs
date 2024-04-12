using UnityEngine;

public class DropLocationFollower : MonoBehaviour
{
    public Transform playerCamera; // Assign the player's camera transform in the inspector

    void Update()
    {
        // Ensure the playerCamera has been assigned
        if (playerCamera != null)
        {
            // Set the forward direction of this GameObject to match the forward direction of the camera
            transform.forward = playerCamera.forward;
        }
    }
}
