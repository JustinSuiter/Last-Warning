using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement settings - these will show up in the Inspector
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;


    // Sprinting settings
    public float sprintSpeed = 8f;
    public float maxStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;
    public float staminaRegenDelay = 1f;

    private float currentStamina;
    private float timeSinceStoppedSprinting = 0f;
    private bool isSprinting = false;

    public float jumpForce = 5f;
    private bool isGrounded = true;
    
    
    private Rigidbody rb;
    private Camera playerCamera;
    private UIManager uiManager;
    
    private float verticalRotation = 0f;
    
    // Called once when the game starts
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        
        
        playerCamera = GetComponentInChildren<Camera>();

        uiManager = FindFirstObjectByType<UIManager>();
        
        currentStamina = maxStamina;
    
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    void Update()
    {
        HandleMouseLook();
        HandleJump();
        HandleStamina();
    
        if (uiManager != null)
        {
            uiManager.UpdateStaminaBar(currentStamina, maxStamina);
        }
    }
    
    
    void FixedUpdate()
    {
        
        HandleMovement();
    }
    
    void HandleMovement()
    {
        
        if (SettingsMenuController.isSettingsOpen)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }
        
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        
        
        moveDirection.Normalize();
        
        
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;
        
        
        Vector3 movement = moveDirection * currentSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }
    
    void HandleMouseLook()
    {
        
        if (SettingsMenuController.isSettingsOpen)
            return;
        
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        
        transform.Rotate(0f, mouseX, 0f);
        
        
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleJump()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void HandleStamina()
    {
        
        if (SettingsMenuController.isSettingsOpen)
            return;
        
        
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift);
        
        
        bool isMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        
        
        if (wantsToSprint && isMoving && currentStamina > 0)
        {
            isSprinting = true;
            
            
            currentStamina -= staminaDrainRate * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0);
            
            
            timeSinceStoppedSprinting = 0f;
        }
        else
        {
            isSprinting = false;
            
            
            timeSinceStoppedSprinting += Time.deltaTime;
            
            
            if (timeSinceStoppedSprinting >= staminaRegenDelay)
            {
                currentStamina += staminaRegenRate * Time.deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }
        }
    }


    public float GetStamina()
    {
        return currentStamina;
    }

    public float GetMaxStamina()
    {
        return maxStamina;
    }
    
    // Check if player is touching the ground
    void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}