using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script moves the camera with player as the player moves
public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
