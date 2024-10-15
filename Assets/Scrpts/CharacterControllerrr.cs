using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCharacterControllerrr : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpHeight = 2f;
    public Transform cameraTransform;
    public GameObject gun; // Public gun object to be assigned in the Inspector

    private CharacterController characterController;
    public InputStateMachine ISM;
    private float gravity = -9.81f;
    public Vector3 velocity;
    private bool isGrounded;
    private float groundCheckDistance = 0.4f;
    public LayerMask groundMask;
    public Transform groundCheck;
    private float currentSwayY = 0f;
    private float currentTiltZ = 0f;
    private float currentTiltX = 0f;

    // Gun sway parameters
    public float swayAmount = 0.05f; // Amount of sway (sloper)
    public float swaySpeed = 2f;     // Speed of sway
    private Vector3 gunInitialPosition;
    private float pitch = 0f; // Store vertical rotation
    public float maxPitch = 90f; // Maximum up/down angle
    public Vector3 velocityCharecter;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        if (gun != null)
        {
            gunInitialPosition = gun.transform.localPosition; // Store the initial position of the gun
        }
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJumpAndGravity();
        HandleGunSway();
        HandleVelocity();
        HandleMouseLook();
    }
    void LateUpdate()
    {

        //HandleMouseLook();


    }
    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (!ISM.isRuning)
        {
            characterController.Move(move * moveSpeed * Time.deltaTime);
        }
        else
        {
            characterController.Move(move * moveSpeed * 2 * Time.deltaTime);
        }
    }
    void HandleVelocity()
    {
        velocityCharecter = characterController.velocity;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -maxPitch, maxPitch);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
    void HandleJumpAndGravity()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reset the falling speed when grounded
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Jump formula
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        ISM.isJumping = !isGrounded;
    }

    void HandleGunSway()
    {
        if (gun == null) return;

        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate target sway value based on movement
        float targetSway = 0;
        if (moveX != 0)
        {
            targetSway = Mathf.Sin(Time.time * swaySpeed) * swayAmount * moveX;
        }
        else if (moveZ != 0)
        {
            targetSway = Mathf.Sin(Time.time * swaySpeed) * swayAmount * moveZ;
        }

        currentSwayY = Mathf.Lerp(currentSwayY, targetSway, Time.deltaTime * swaySpeed);

        gun.transform.localPosition = gunInitialPosition + new Vector3(0, currentSwayY, 0);




        float targetTiltZ = Mathf.Lerp(0, 13f, Mathf.Abs(moveX)) * Mathf.Sign(moveX);
        float targetTiltX = Mathf.Lerp(0, 4f, Mathf.Abs(moveZ)) * Mathf.Sign(moveZ);

        currentTiltZ = Mathf.Lerp(currentTiltZ, targetTiltZ, Time.deltaTime * swaySpeed);
        currentTiltX = Mathf.Lerp(currentTiltX, targetTiltX, Time.deltaTime * swaySpeed);

        gun.transform.localRotation = Quaternion.Euler(currentTiltX, gun.transform.localRotation.eulerAngles.y, currentTiltZ);

    }

}
