using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Controller Parameters")]
    public CharacterController characterController;

    [Header("Movement Parameters")]
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 2f;
    public float groundDistance = 0.4f;

    [Header("Transform Parameters")]
    public Transform groundCheck;

    [Header("Layer Mask Parameters")]
    public LayerMask groundLayerMask;



    Vector3 velocity;

    bool isGrounded;

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        velocity.y += gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tiger"))
        {
            GetComponent<BarManager>().GetDamage(7.5f, 0.05f);
        }
    }
}
