using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class UIManager : MonoBehaviour
{
    
    public TextMeshProUGUI partsText;
    public TextMeshProUGUI timerText;
    public Image staminaBarFill;
    
    
    public void UpdatePartsUI(int collected, int total)
    {
        partsText.text = "Parts: " + collected + "/" + total;
    }
    
    
    public void UpdateTimerUI(float timeRemaining)
    {
        
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        
        
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
        
        // Change color based on time remaining
        if (timeRemaining <= 60f)
        {
            timerText.color = Color.red;
        }
        else if (timeRemaining <= 120f)
        {
            timerText.color = Color.yellow;
        }
        else
        {
            timerText.color = Color.white;
        }
    }

        public void UpdateStaminaBar(float currentStamina, float maxStamina)
    {
        if (staminaBarFill != null)
        {
            float fillAmount = currentStamina / maxStamina;
            staminaBarFill.fillAmount = fillAmount;
        }
    }
}