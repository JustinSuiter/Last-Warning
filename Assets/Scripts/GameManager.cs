using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Track how many parts we've collected
    public int partsCollected = 0;
    public int totalPartsNeeded = 5;
    
    // Reference to the UI Manager (Person B will create this)
    private UIManager uiManager;
    
    void Start()
    {
        // Find the UI Manager in the scene
        uiManager = FindFirstObjectByType<UIManager>();
        
        // Update the UI at the start
        if (uiManager != null)
        {
            uiManager.UpdatePartsUI(partsCollected, totalPartsNeeded);
        }
    }
    
    // This gets called by Collectible objects when picked up
    public void CollectPart()
    {
        partsCollected++;
        
        Debug.Log("Parts collected: " + partsCollected + "/" + totalPartsNeeded);
        
        // Update the UI
        if (uiManager != null)
        {
            uiManager.UpdatePartsUI(partsCollected, totalPartsNeeded);
        }
        
        // Check if we got all parts
        if (partsCollected >= totalPartsNeeded)
        {
            Debug.Log("All parts collected! Bunker door can open!");
            // We'll add door opening logic on Day 5
        }
    }
    
    // Method for when player loses (Person B will call this from timer)
    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; // Pause the game
        // We'll add lose screen later
    }
}