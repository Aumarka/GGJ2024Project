using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float speed = 10.0f;
    public float speedBoostMultiplier = 1.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    [HideInInspector]
    public Vector3 moveDirection = Vector3.zero;
    private float movementPenalty = 1.0f;
    private GameObject movementPenaliser;

    private float speedMultiplier;
    private CharacterController characterController;
    private Vector2 rotation = Vector2.zero;
    private bool canMove = true;

    [Header("Player Camera Settings")]
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 90.0f;

    

    void Start()
    {
        // Set character controller settings
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;

        movementPenalty = 1.0f;
        movementPenaliser = null;
    }

    void Update()
    {
        PlayerMovement(); 
    }

    void PlayerMovement()
    {
        if (movementPenaliser == null)
        {
            movementPenalty = 1.0f;
        }

        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * speedMultiplier * movementPenalty * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * speedMultiplier * movementPenalty * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }

            if (Input.GetKey(KeyCode.LeftShift) && canMove)
            {
                speedMultiplier = speedBoostMultiplier;
            }
            else
            {
                speedMultiplier = 1.0f;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
    }
}
