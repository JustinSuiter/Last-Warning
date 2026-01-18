using UnityEngine;
using TMPro; // This lets us use TextMeshPro

public class UIManager : MonoBehaviour
{
    // References to our UI text elements
    public TextMeshProUGUI partsText;
    public TextMeshProUGUI timerText;
    
    // Called by GameManager when parts are collected
    public void UpdatePartsUI(int collected, int total)
    {
        partsText.text = "Parts: " + collected + "/" + total;
    }
    
    // Called every frame by TimerManager to update the timer display
    public void UpdateTimerUI(float timeRemaining)
    {
        // Convert seconds to minutes:seconds format
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        
        // Display with leading zero if needed (5:09 instead of 5:9)
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        
        // Change color based on time remaining
        if (timeRemaining <= 60f)
        {
            timerText.color = Color.red; // Last minute = red
        }
        else if (timeRemaining <= 120f)
        {
            timerText.color = Color.yellow; // Two minutes = yellow
        }
        else
        {
            timerText.color = Color.white; // Otherwise white
        }
    }
}