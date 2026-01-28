using UnityEngine;
using System.Collections;

public class BunkerDoorController : MonoBehaviour
{
    public Transform door;
    public GameObject doorLight;
    public Material lockedMaterial;
    public Material unlockedMaterial;
    public Camera cinematicCamera;
    public Camera playerCamera;
    
    public float doorOpenHeight = 3.5f;
    public float doorOpenSpeed = 2f;
    public float cinematicDuration = 3f;
    
    private bool isUnlocked = false;
    private bool isOpening = false;
    private bool hasEntered = false;
    private bool cinematicPlayed = false;
    private Vector3 doorClosedPosition;
    private Vector3 doorOpenPosition;
    private GameManager gameManager;
    private PlayerController playerController;
    
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        
        playerController = FindFirstObjectByType<PlayerController>();
        
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
        
        if (door != null)
        {
            doorClosedPosition = door.localPosition;
            doorOpenPosition = doorClosedPosition + Vector3.up * doorOpenHeight;
        }
        
        UpdateLightColor(false);
        
        if (cinematicCamera != null)
        {
            cinematicCamera.enabled = false;
        }
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
        
        Debug.Log("Bunker door unlocked! Playing cinematic...");
        
        StartCoroutine(PlayDoorCinematic());
    }
    
    IEnumerator PlayDoorCinematic()
    {
        cinematicPlayed = true;
        
        if (playerController != null)
        {
            playerController.enabled = false; // Disables player input
        }
        
        if (playerCamera != null)
        {
            playerCamera.enabled = false;
        }
        if (cinematicCamera != null)
        {
            cinematicCamera.enabled = true;
        }
        
        yield return new WaitForSeconds(0.5f);
        
        UpdateLightColor(true);
        isOpening = true;
        
        yield return new WaitForSeconds(cinematicDuration);
        
        if (cinematicCamera != null)
        {
            cinematicCamera.enabled = false;
        }
        if (playerCamera != null)
        {
            playerCamera.enabled = true;
        }
        
        if (playerController != null)
        {
            playerController.enabled = true;
        }
        
        Debug.Log("Cinematic complete. Player control restored.");
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