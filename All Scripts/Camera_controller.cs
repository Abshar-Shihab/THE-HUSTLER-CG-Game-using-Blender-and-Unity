using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_controller : MonoBehaviour
{ 
    public Transform player; // The transform of the player
    public float distance = 1.21f; // The distance between the player and the camera
    public float height = 1.67f; // The height of the camera above the player
    public float xAxisMovement = 0.0f; // The movement of the camera along the x-axis
    public float rotationSpeed = 5.0f; // The speed at which the camera rotates around the player
    public float smoothSpeed = 5.0f; // The speed at which the camera moves and rotates
    public float zoomDistance = -0.77f;
    public float zoomHeight = 1.67f;

    void Update()
    {
        // Get the player's transform
        Transform playerTransform = player.transform;

        // Rotate the camera around the player
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        playerTransform.Rotate(Vector3.up, mouseX);

        // Calculate the desired position for the camera
        Vector3 cameraPosition = playerTransform.position;
        cameraPosition -= playerTransform.forward * distance;
        cameraPosition.y = playerTransform.position.y + height;
        cameraPosition.x += xAxisMovement;

        //dsfdfd
        if (Input.GetMouseButton(1))
        {
            // Zoom the camera in
            cameraPosition -= playerTransform.forward * zoomDistance;
            cameraPosition.y = playerTransform.position.y + zoomHeight;
        }
        else
        {
            // Return the camera to the default position
            cameraPosition -= playerTransform.forward * distance;
            cameraPosition.y = playerTransform.position.y + height;
        }

        // Smoothly move the camera to the desired position and rotation
        transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerTransform.rotation, Time.deltaTime * smoothSpeed);
    }
}

