using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WinManager : MonoBehaviour
{
    public GameObject winPanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI statsText;
    
    void Start()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
    }
    
    public void ShowWinScreen(int partsCollected, int totalParts, float completionTime)
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
        
        if (messageText != null)
        {
            if (completionTime < 180f)
            {
                messageText.text = "Incredible speed! You're a survivor!";
            }
            else if (completionTime < 240f)
            {
                messageText.text = "You escaped the apocalypse!";
            }
            else
            {
                messageText.text = "You made it... barely!";
            }
        }
        
        if (statsText != null)
        {
            int minutes = Mathf.FloorToInt(completionTime / 60f);
            int seconds = Mathf.FloorToInt(completionTime % 60f);
            
            statsText.text = string.Format("Parts Collected: {0}/{1}\nCompletion Time: {2}:{3:00}", 
                                          partsCollected, totalParts, minutes, seconds);
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 0f;
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