using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Mouse_3D : MonoBehaviour
{
    public Camera cam;
    //public float mouseSensitivity = 100f;
    // The sensitivity of the mouse or controller input
    public float sensitivity = 100f;

    // The minimum and maximum pitch (up/down) angle of the camera
    public float pitchMin = -15f;
    public float pitchMax = 10f;

    // The current pitch angle of the camera
    private float pitch = 0f;
    void Update()
    {
        /*float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime, -80f, 80f);


        transform.Rotate(Vector3.up * mouseX * 2);
        cam.transform.Rotate(-Vector3.right * mouseY);
        cam.transform.Rotate(Vector3.up * mouseX, Space.World);*/

        // Get the horizontal and vertical input
        float yaw = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Clamp the pitch angle
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Rotate the camera
        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y + yaw, 0f);
    }
}
