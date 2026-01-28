using UnityEngine;

public class BunkerDoorController : MonoBehaviour
{
    public Transform door;
    public GameObject doorLight;
    public Material lockedMaterial;
    public Material unlockedMaterial;
    
    public float doorOpenHeight = 3.5f;
    public float doorOpenSpeed = 2f;
    
    private bool isUnlocked = false;
    private bool isOpening = false;
    private bool hasEntered = false;
    private Vector3 doorClosedPosition;
    private Vector3 doorOpenPosition;
    private GameManager gameManager;
    
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        if (door != null)
        {
            doorClosedPosition = door.localPosition;
            doorOpenPosition = doorClosedPosition + Vector3.up * doorOpenHeight;
        }
        
        UpdateLightColor(false);
    }
    
    void Update()
    {
        if (!isUnlocked && gameManager != null)
        {
            if (gameManager.partsCollected >= gameManager.totalPartsNeeded)
            {
                UnlockDoor();
            }
        }
        
        if (isOpening && door != null)
        {
            door.localPosition = Vector3.Lerp(door.localPosition, doorOpenPosition, Time.deltaTime * doorOpenSpeed);
            
            if (Vector3.Distance(door.localPosition, doorOpenPosition) < 0.1f)
            {
                isOpening = false;
                door.localPosition = doorOpenPosition;
            }
        }
    }
    
    void UnlockDoor()
    {
        isUnlocked = true;
        isOpening = true;
        
        UpdateLightColor(true);
        
        Debug.Log("Bunker door unlocked and opening!");
    }
    
    void UpdateLightColor(bool unlocked)
    {
        if (doorLight == null)
            return;
        
        Renderer lightRenderer = doorLight.GetComponent<Renderer>();
        if (lightRenderer != null)
        {
            if (unlocked && unlockedMaterial != null)
            {
                lightRenderer.material = unlockedMaterial;
            }
            else if (!unlocked && lockedMaterial != null)
            {
                lightRenderer.material = lockedMaterial;
            }
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (isUnlocked && !hasEntered && other.CompareTag("Player"))
        {
            hasEntered = true;
            Debug.Log("Player entered bunker - YOU WIN!");
            
            if (gameManager != null)
            {
                gameManager.PlayerWon();
            }
        }
        else if (!isUnlocked && other.CompareTag("Player"))
        {
            Debug.Log("Door is locked! Collect all parts first.");
        }
    }
}