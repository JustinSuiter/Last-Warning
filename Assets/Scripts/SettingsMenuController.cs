using UnityEngine;

public class SettingsMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    private bool isOpen = false;

    public static bool isSettingsOpen = false;
    
    void Start()
    {
        settingsPanel.SetActive(false);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }
    
    public void ToggleMenu()
    {
        isOpen = !isOpen;
        settingsPanel.SetActive(isOpen);
    
        isSettingsOpen = isOpen;
    
        Time.timeScale = isOpen ? 0f : 1f;
    
        Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isOpen;
    }
    
    public void CloseMenu()
    {
        isOpen = false;
        settingsPanel.SetActive(false);
    
        isSettingsOpen = false;
    
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}