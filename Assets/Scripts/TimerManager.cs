using UnityEngine;

public class TimerManager : MonoBehaviour
{
    // How long the player has to complete the game (in seconds)
    public float totalTime = 300f; // 5 minutes = 300 seconds
    
    private float timeRemaining;
    private UIManager uiManager;
    private GameManager gameManager;
    private bool timerRunning = true;
    
    void Start()
    {
        // Start with full time
        timeRemaining = totalTime;
        
        // Find the managers in the scene
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        
        // Update UI immediately
        if (uiManager != null)
        {
            uiManager.UpdateTimerUI(timeRemaining);
        }
    }
    
    void Update()
    {
        // Only count down if timer is running
        if (!timerRunning)
            return;
        
        // Reduce time remaining
        timeRemaining -= Time.deltaTime;
        
        // Update the UI
        if (uiManager != null)
        {
            uiManager.UpdateTimerUI(timeRemaining);
        }
        
        // Check if time ran out
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            
            // Tell GameManager the player lost
            if (gameManager != null)
            {
                gameManager.GameOver();
            }
            
            Debug.Log("Time's up! Nuke incoming!");
        }
    }
    
    // Method to stop the timer (we'll use this when player wins)
    public void StopTimer()
    {
        timerRunning = false;
    }
}