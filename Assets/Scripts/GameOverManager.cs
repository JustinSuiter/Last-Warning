using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI reasonText;
    public TextMeshProUGUI statsText;
    
    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }
    
    public void ShowGameOver(string reason, int partsCollected, int totalParts, float timeSurvived)
    {
        Debug.Log("=== SHOW GAME OVER CALLED ===");
        
        if (gameOverPanel != null)
        {
            Debug.Log("Activating GameOverPanel");
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("gameOverPanel is NULL!");
        }
        
        if (reasonText != null)
        {
            reasonText.text = reason;
            Debug.Log("Set reason text to: " + reason);
        }
        else
        {
            Debug.LogError("reasonText is NULL!");
        }
        
        if (statsText != null)
        {
            int minutes = Mathf.FloorToInt(timeSurvived / 60f);
            int seconds = Mathf.FloorToInt(timeSurvived % 60f);
            
            statsText.text = string.Format("Parts Collected: {0}/{1}\nTime Survived: {2}:{3:00}", 
                                        partsCollected, totalParts, minutes, seconds);
            Debug.Log("Set stats text");
        }
        else
        {
            Debug.LogError("statsText is NULL!");
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 0f;
        
        Debug.Log("=== SHOW GAME OVER COMPLETE ===");
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}