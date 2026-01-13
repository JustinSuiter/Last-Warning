using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement settings - these will show up in the Inspector
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    
    // We'll store references to components we need
    private Rigidbody rb;
    private Camera playerCamera;
    
    // Track camera rotation
    private float verticalRotation = 0f;
    
    // Called once when the game starts
    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();
        
        // Get the Camera that's a child of this GameObject
        playerCamera = GetComponentInChildren<Camera>();
        
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    // Called every frame
    void Update()
    {
        // Handle camera rotation (mouse look)
        HandleMouseLook();
        
        // Press Escape to unlock cursor (useful for testing)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    // Called at fixed time intervals (better for physics)
    void FixedUpdate()
    {
        // Handle player movement (WASD)
        HandleMovement();
    }
    
    void HandleMovement()
    {
        // Get input from WASD or arrow keys (-1 to 1)
        float horizontalInput = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxis("Vertical");     // W/S or Up/Down
        
        // Calculate movement direction relative to where player is facing
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        
        // Normalize so diagonal movement isn't faster
        moveDirection.Normalize();
        
        // Apply movement to Rigidbody
        Vector3 movement = moveDirection * moveSpeed;
        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
        // Note: We keep the Y velocity so gravity still works
    }
    
    void HandleMouseLook()
    {
        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Rotate the player left/right
        transform.Rotate(0f, mouseX, 0f);
        
        // Rotate the camera up/down
        verticalRotation -= mouseY; // Subtract so moving mouse up looks up
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f); // Limit to prevent flipping
        
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}