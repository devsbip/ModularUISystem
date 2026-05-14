using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the UI interactions for the settings menu, acting as the View layer.
/// </summary>
public class SettingsPanel : UIPanel
{
    [Header("Audio Controls")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Video Controls")]
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TMP_Dropdown _qualityDropdown;
    [SerializeField] private Toggle _fullscreenToggle;

    [Header("Nav")]
    [SerializeField] private Button _backButton;

    void Start()
    {
        _backButton.onClick.AddListener(OnBackClicked);

        InitializeResolutionDropdown();
        InitializeQualityDropdown();
    }

    public override void Show()
    {
        base.Show();
        LoadCurrentSettingsToUI();
    }

    private void LoadCurrentSettingsToUI()
    {
        // Remove all listeners to prevent logic execution while setting UI values
        _masterSlider.onValueChanged.RemoveAllListeners();
        _musicSlider.onValueChanged.RemoveAllListeners();
        _sfxSlider.onValueChanged.RemoveAllListeners();

        _resolutionDropdown.onValueChanged.RemoveAllListeners();
        _qualityDropdown.onValueChanged.RemoveAllListeners();
        _fullscreenToggle.onValueChanged.RemoveAllListeners();

        // Fetch data from the Manager
        SettingsData currentData = SettingsManager.Instance.Data;

        // Apply Audio UI
        _masterSlider.value = currentData.MasterVolume;
        _musicSlider.value = currentData.MusicVolume;
        _sfxSlider.value = currentData.SFXVolume;

        // Apply Video UI
        _resolutionDropdown.value = currentData.ResolutionIndex;
        _resolutionDropdown.RefreshShownValue();

        _qualityDropdown.value = currentData.QualityIndex;
        _qualityDropdown.RefreshShownValue();

        _fullscreenToggle.isOn = currentData.IsFullscreen;

        // Re-add the listeners
        _masterSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _sfxSlider.onValueChanged.AddListener(OnSFXVolumeChanged);

        _resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
        _qualityDropdown.onValueChanged.AddListener(OnQualityChanged);
        _fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void InitializeResolutionDropdown()
    {
        _resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        Resolution[] resolutions = SettingsManager.Instance.AvailableResolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(option);
        }

        _resolutionDropdown.AddOptions(options);
    }

    private void InitializeQualityDropdown()
    {
        _qualityDropdown.ClearOptions();
        List<string> options = new List<string>(QualitySettings.names);
        _qualityDropdown.AddOptions(options);
    }

    // Audio Handlers
    private void OnMasterVolumeChanged(float value)
    {
        Debug.Log($"[UI] Slider Master movido para: {value}");
        SettingsManager.Instance.SetMasterVolume(value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        SettingsManager.Instance.SetMusicVolume(value);
    }

    private void OnSFXVolumeChanged(float value)
    {
        SettingsManager.Instance.SetSFXVolume(value);
    }

    // Video Handlers
    private void OnResolutionChanged(int index)
    {
        SettingsManager.Instance.SetResolution(index);
    }

    private void OnQualityChanged(int index)
    {
        SettingsManager.Instance.SetQuality(index);
    }

    private void OnFullscreenChanged(bool isFullscreen)
    {
        SettingsManager.Instance.SetFullscreen(isFullscreen);
    }

    // Nav
    private void OnBackClicked()
    {
        SettingsManager.Instance.SaveSettings();
        UIManager.Instance.GoBack();
    }
}
