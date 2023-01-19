using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Rigidbody : MonoBehaviour
{
    public float speed = 5.0f; // The speed at which the character moves
    public float jumpForce = 10.0f; // The force applied to the character when jumping
    public float gravity = 20.0f; // The gravity applied to the character
    public float rotateSpeed = 5.0f; // The speed at which the character rotates

    private Rigidbody rb; // The character's Rigidbody component
    private Animator anim; // The character's Animator component
    private bool isGrounded; // Whether the character is currently grounded

    void Start()
    {
        // Get the Rigidbody and Animator components
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the character is grounded
        isGrounded = Physics.Raycast(transform.position, -Vector3.up, 0.1f);

        // Get the input axis
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate the direction the character will move
        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);

        // Normalize the direction to prevent diagonal movement from being faster
        direction = direction.normalized;

        // Check if the direction vector is a zero vector
        if (direction.sqrMagnitude > 0.0f)
        {
            // Normalize the direction to prevent diagonal movement from being faster
            direction = direction.normalized;

            // Rotate the character towards the direction of movement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotateSpeed);
        }

        // Move the character in the direction
        rb.velocity = direction * speed + new Vector3(0.0f, rb.velocity.y, 0.0f);

        // Set the animator's "Speed" parameter based on the character's velocity
        anim.SetFloat("Speed", rb.velocity.magnitude);

        // Check if the player is pressing the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply a force to the character to make it jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Apply gravity to the character
        rb.AddForce(Vector3.down * gravity);
    }
}
