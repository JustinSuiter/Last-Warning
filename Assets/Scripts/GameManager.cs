using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int partsCollected = 0;
    public int totalPartsNeeded = 5;
    
    private UIManager uiManager;
    
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        
        if (uiManager != null)
        {
            uiManager.UpdatePartsUI(partsCollected, totalPartsNeeded);
        }
    }
    
    public void CollectPart()
    {
        partsCollected++;
        
        Debug.Log("Parts collected: " + partsCollected + "/" + totalPartsNeeded);
        
        if (uiManager != null)
        {
            uiManager.UpdatePartsUI(partsCollected, totalPartsNeeded);
        }
        
        if (partsCollected >= totalPartsNeeded)
        {
            Debug.Log("All parts collected! Bunker door can open!");
        }
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f;
    }

}