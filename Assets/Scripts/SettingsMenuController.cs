using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject settingsPanel; // The panel we show/hide
    private bool isOpen = false;

    public static bool isSettingsOpen = false;
    
    void Start()
    {
        // Start with menu closed
        settingsPanel.SetActive(false);
    }
    
    void Update()
    {
        // Press Escape to toggle menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    
    public void ToggleMenu()
    {
        isOpen = !isOpen;
        settingsPanel.SetActive(isOpen);
    
        // Update the static variable
        isSettingsOpen = isOpen;
    
        // Pause game when menu is open
        Time.timeScale = isOpen ? 0f : 1f;
    
        // Show/hide cursor
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }
    
    // Call this from the Close button
    public void CloseMenu()
    {
        isOpen = false;
        settingsPanel.SetActive(false);
    
        // Update the static variable
        isSettingsOpen = false;
    
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}