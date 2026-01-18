using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    // References to UI sliders
    public Slider fovSlider;
    public Slider sensitivitySlider;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    
    // References to text labels showing current values
    public TextMeshProUGUI fovValueText;
    public TextMeshProUGUI sensitivityValueText;
    public TextMeshProUGUI masterVolumeValueText;
    public TextMeshProUGUI sfxVolumeValueText;
    
    // Default values
    private float defaultFOV = 60f;
    private float defaultSensitivity = 2f;
    private float defaultMasterVolume = 0.8f;
    private float defaultSFXVolume = 0.8f;
    
    void Start()
    {
        LoadSettings();
        ApplySettings();
    }
    
    // Load saved settings from PlayerPrefs (Unity's save system)
    void LoadSettings()
    {
        // Load FOV (default to 60 if not saved)
        float savedFOV = PlayerPrefs.GetFloat("FOV", defaultFOV);
        fovSlider.value = savedFOV;
        
        // Load Sensitivity
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;
        
        // Load Master Volume
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume);
        masterVolumeSlider.value = savedMasterVolume;
        
        // Load SFX Volume
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", defaultSFXVolume);
        sfxVolumeSlider.value = savedSFXVolume;
    }
    
    // Apply settings to the game
    void ApplySettings()
    {
        // Find the player's camera and update FOV
        Camera playerCamera = FindFirstObjectByType<PlayerController>().GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = fovSlider.value;
        }
        
        // Find player controller and update sensitivity
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.mouseSensitivity = sensitivitySlider.value;
        }
        
        // Apply volume settings
        AudioListener.volume = masterVolumeSlider.value;
        
        UpdateUILabels();
    }
    
    // Update the text labels next to sliders
    void UpdateUILabels()
    {
        fovValueText.text = fovSlider.value.ToString("F0"); // No decimals
        sensitivityValueText.text = sensitivitySlider.value.ToString("F1"); // 1 decimal
        masterVolumeValueText.text = (masterVolumeSlider.value * 100).ToString("F0") + "%";
        sfxVolumeValueText.text = (sfxVolumeSlider.value * 100).ToString("F0") + "%";
    }
    
    // Called when FOV slider changes
    public void OnFOVChanged()
    {
        PlayerPrefs.SetFloat("FOV", fovSlider.value);
        ApplySettings();
    }
    
    // Called when Sensitivity slider changes
    public void OnSensitivityChanged()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        ApplySettings();
    }
    
    // Called when Master Volume slider changes
    public void OnMasterVolumeChanged()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        ApplySettings();
    }
    
    // Called when SFX Volume slider changes
    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        ApplySettings();
    }
    
    // Reset all settings to defaults
    public void ResetToDefaults()
    {
        fovSlider.value = defaultFOV;
        sensitivitySlider.value = defaultSensitivity;
        masterVolumeSlider.value = defaultMasterVolume;
        sfxVolumeSlider.value = defaultSFXVolume;
        
        PlayerPrefs.SetFloat("FOV", defaultFOV);
        PlayerPrefs.SetFloat("Sensitivity", defaultSensitivity);
        PlayerPrefs.SetFloat("MasterVolume", defaultMasterVolume);
        PlayerPrefs.SetFloat("SFXVolume", defaultSFXVolume);
        
        ApplySettings();
    }
}