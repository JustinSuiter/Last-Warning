using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public float damage = 50f;
    public float swingCooldown = 1f;
    public float swingStaminaCost = 15f;
    public float swingRange = 2f;
    
    public float swingSpeed = 10f;
    public float swingAngle = 45f;
    
    private float lastSwingTime = -999f;
    private bool isSwinging = false;
    private float swingProgress = 0f;
    private Quaternion originalRotation;
    private PlayerController playerController;
    
    void Start()
    {
        originalRotation = transform.localRotation;
        
        playerController = GetComponentInParent<PlayerController>();
    }
    
    void Update()
    {
        if (Time.timeScale == 0f)
            return;
        
        if (isSwinging)
        {
            AnimateSwing();
        }
        else
        {
            // Check for attack input (left mouse button)
            if (Input.GetMouseButtonDown(0))
            {
                TrySwing();
            }
        }
    }
    
    void TrySwing()
    {
        if (Time.time < lastSwingTime + swingCooldown)
        {
            Debug.Log("Weapon on cooldown!");
            return;
        }
        
        if (playerController != null && playerController.GetStamina() < swingStaminaCost)
        {
            Debug.Log("Not enough stamina to swing!");
            return;
        }
        
        isSwinging = true;
        swingProgress = 0f;
        lastSwingTime = Time.time;
        
        if (playerController != null)
        {
            playerController.UseStamina(swingStaminaCost);
        }
        
        CheckForHits();
        
        Debug.Log("Swing!");
    }
    
    void AnimateSwing()
    {
        swingProgress += Time.deltaTime * swingSpeed;
        
        if (swingProgress < 1f)
        {
            float angle = Mathf.Lerp(0, swingAngle, swingProgress);
            transform.localRotation = originalRotation * Quaternion.Euler(-angle, 0, 0);
        }
        else if (swingProgress < 2f)
        {
            float angle = Mathf.Lerp(swingAngle, 0, swingProgress - 1f);
            transform.localRotation = originalRotation * Quaternion.Euler(-angle, 0, 0);
        }
        else
        {
            transform.localRotation = originalRotation;
            isSwinging = false;
        }
    }
    
    void CheckForHits()
    {

        Camera playerCamera = GetComponentInParent<Camera>();
        if (playerCamera == null)
            return;
        
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, swingRange))
        {
            WolfAI wolf = hit.collider.GetComponent<WolfAI>();
            if (wolf != null)
            {
                wolf.TakeDamage(damage);
                Debug.Log("Hit wolf for " + damage + " damage!");
            }
        }
    }
}