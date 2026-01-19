using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int partsCollected = 0;
    public int totalPartsNeeded = 5;
    private UIManager uiManager;
    private GameOverManager gameOverManager;
    private float gameStartTime;
    
    void Start()
    {
        uiManager = FindFirstObjectByType<UIManager>();
        gameOverManager = FindFirstObjectByType<GameOverManager>();
        
        gameStartTime = Time.time;
        
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
    
    public void GameOver(string reason)
    {
        Debug.Log("=== GAME OVER CALLED === Reason: " + reason);
        
        float timeSurvived = Time.time - gameStartTime;
        
        Debug.Log("Time survived: " + timeSurvived);
        
        if (gameOverManager != null)
        {
            Debug.Log("Found GameOverManager, calling ShowGameOver...");
            gameOverManager.ShowGameOver(reason, partsCollected, totalPartsNeeded, timeSurvived);
        }
        else
        {
            Debug.LogError("GameOverManager NOT FOUND!");
        }
        
        Time.timeScale = 0f;
        Debug.Log("Time.timeScale set to 0");
    }
}