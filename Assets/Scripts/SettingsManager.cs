using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public Slider fovSlider;
    public Slider sensitivitySlider;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    
    public TextMeshProUGUI fovValueText;
    public TextMeshProUGUI sensitivityValueText;
    public TextMeshProUGUI masterVolumeValueText;
    public TextMeshProUGUI sfxVolumeValueText;
    
    private float defaultFOV = 60f;
    private float defaultSensitivity = 2f;
    private float defaultMasterVolume = 0.8f;
    private float defaultSFXVolume = 0.8f;
    
    void Start()
    {
        LoadSettings();
        ApplySettings();
    }
    
    void LoadSettings()
    {
        float savedFOV = PlayerPrefs.GetFloat("FOV", defaultFOV);
        fovSlider.value = savedFOV;
        
        float savedSensitivity = PlayerPrefs.GetFloat("Sensitivity", defaultSensitivity);
        sensitivitySlider.value = savedSensitivity;
        
        float savedMasterVolume = PlayerPrefs.GetFloat("MasterVolume", defaultMasterVolume);
        masterVolumeSlider.value = savedMasterVolume;
        
        float savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", defaultSFXVolume);
        sfxVolumeSlider.value = savedSFXVolume;
    }
    
    void ApplySettings()
    {
        Camera playerCamera = FindFirstObjectByType<PlayerController>().GetComponentInChildren<Camera>();
        if (playerCamera != null)
        {
            playerCamera.fieldOfView = fovSlider.value;
        }
        
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            player.mouseSensitivity = sensitivitySlider.value;
        }
        
        AudioListener.volume = masterVolumeSlider.value;
        
        UpdateUILabels();
    }
    
    void UpdateUILabels()
    {
        fovValueText.text = fovSlider.value.ToString("F0"); // No decimals
        sensitivityValueText.text = sensitivitySlider.value.ToString("F1"); // 1 decimal
        masterVolumeValueText.text = (masterVolumeSlider.value * 100).ToString("F0") + "%";
        sfxVolumeValueText.text = (sfxVolumeSlider.value * 100).ToString("F0") + "%";
    }
    
    public void OnFOVChanged()
    {
        PlayerPrefs.SetFloat("FOV", fovSlider.value);
        ApplySettings();
    }
    
    public void OnSensitivityChanged()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        ApplySettings();
    }
    
    public void OnMasterVolumeChanged()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        ApplySettings();
    }
    
    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxVolumeSlider.value);
        ApplySettings();
    }
    
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