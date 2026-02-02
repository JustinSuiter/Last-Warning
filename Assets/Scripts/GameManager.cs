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
        float timeSurvived = Time.time - gameStartTime;
        
        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver(reason, partsCollected, totalPartsNeeded, timeSurvived);
        }
        
        Time.timeScale = 0f;
    }

    public void PlayerWon()
    {
        float timeSurvived = Time.time - gameStartTime;
        
        Debug.Log("Player Won! Time: " + timeSurvived);
        
        WinManager winManager = FindFirstObjectByType<WinManager>();
        if (winManager != null)
        {
            winManager.ShowWinScreen(partsCollected, totalPartsNeeded, timeSurvived);
        }
        
        Time.timeScale = 0f;
    }
}