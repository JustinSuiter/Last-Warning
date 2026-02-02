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
    public float maxHealth = 100f;
    public float currentHealth;

    public float jumpForce = 5f;
    private bool isGrounded = true;
    
    
    private Rigidbody rb;
    private Camera playerCamera;
    private UIManager uiManager;
    private GameManager gameManager;
    
    private float verticalRotation = 0f;
    private bool isDead = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        
        uiManager = FindFirstObjectByType<UIManager>();
        
        gameManager = FindFirstObjectByType<GameManager>(); // Add this line
        
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in scene! Make sure GameManager GameObject exists.");
        }
        
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        
        if (uiManager != null)
        {
            uiManager.UpdateHealthBar(currentHealth, maxHealth);
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    
    void Update()
    {
        if (isDead)
        return;
        CheckGrounded();
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
        if (isDead)
        return;
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

    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        currentStamina = Mathf.Max(currentStamina, 0);
    }
    
    void CheckGrounded()
    {
        float rayDistance = 1.1f;
        
        if (Physics.Raycast(transform.position, Vector3.down, rayDistance))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        
        Debug.Log("Player took " + damage + " damage. Health: " + currentHealth);

        if (uiManager != null)
        {
            uiManager.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die("You were killed by a wolf!"); // Pass the wolf death message
        }
    }

    void Die(string reason = "You died!")
    {
        Debug.Log("=== PLAYER DIED === Reason: " + reason);
        
        isDead = true;
        
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
        
        if (gameManager != null)
        {
            Debug.Log("Found GameManager, calling GameOver...");
            gameManager.GameOver(reason);
        }
        else
        {
            Debug.LogError("GameManager NOT FOUND! Check that GameManager exists in the scene.");
        }
    }
}