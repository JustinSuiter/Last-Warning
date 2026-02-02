using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public float totalTime = 300f;
    
    private float timeRemaining;
    private UIManager uiManager;
    private GameManager gameManager;
    private bool timerRunning = true;
    
    void Start()
    {
        timeRemaining = totalTime;
        
        uiManager = FindFirstObjectByType<UIManager>();
        gameManager = FindFirstObjectByType<GameManager>();
        
        if (uiManager != null)
        {
            uiManager.UpdateTimerUI(timeRemaining);
        }
    }
    
    void Update()
    {
        if (!timerRunning)
            return;
        
        timeRemaining -= Time.deltaTime;
        
        if (uiManager != null)
        {
            uiManager.UpdateTimerUI(timeRemaining);
        }
        
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            
            if (gameManager != null)
            {
                gameManager.GameOver("The nuke detonated!");
            }
        }
    }
    
    public void StopTimer()
    {
        timerRunning = false;
    }
}