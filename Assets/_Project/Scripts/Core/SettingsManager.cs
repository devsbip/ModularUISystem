using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem.Processors;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private AudioMixer _mainAudioMixer;

    private const string SETTINGS_FILE_NAME = "user_settings.json";

    public SettingsData Data { get; private set; }
    public Resolution[] AvailableResolutions { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AvailableResolutions = Screen.resolutions;

        LoadAndApplySettings();
    }

    private void LoadAndApplySettings()
    {
        Data = SaveSystem.Load<SettingsData>(SETTINGS_FILE_NAME);

        if (Data.ResolutionIndex >= AvailableResolutions.Length)
        {
            Data.ResolutionIndex = AvailableResolutions.Length - 1;
        }

        ApplyAllSettings();
    }

    public void SaveSettings()
    {
        SaveSystem.Save(Data, SETTINGS_FILE_NAME);
    }

    public void ApplyAllSettings()
    {
        // Video Logic
        SetQuality(Data.QualityIndex);
        SetFullscreen(Data.IsFullscreen);
        SetResolution(Data.ResolutionIndex);

        // Audio Logic
        SetMasterVolume(Data.MasterVolume);
        SetMusicVolume(Data.MusicVolume);
        SetSFXVolume(Data.SFXVolume);
    }

    // Audio Methods
    private float LinearToDecibel(float linearValue)
    {
        float clampedValue = Mathf.Clamp(linearValue, 0.0001f, 1f);
        return Mathf.Log10(clampedValue) * 20f;
    }

    /// <summary>
    /// Applies Master volume changes to the AudioMixer.
    /// </summary>
    public void SetMasterVolume(float volume)
    {
        Data.MasterVolume = volume;
        _mainAudioMixer.SetFloat("MasterVolume", LinearToDecibel(volume));
    }

    /// <summary>
    /// Applies Music volume changes to the AudioMixer.
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        Data.MusicVolume = volume;
        _mainAudioMixer.SetFloat("MusicVolume", LinearToDecibel(volume));
    }

    /// <summary>
    /// Applies SFX volume changes to the AudioMixer.
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        Data.SFXVolume = volume;
        _mainAudioMixer.SetFloat("SFXVolume", LinearToDecibel(volume));
    }

    // Video Methods

    /// <summary>
    /// Applies the selected graphics quality level.
    /// </summary>
    public void SetQuality(int qualityIndex)
    {
        Data.QualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    /// <summary>
    /// Toggles fullscreen mode.
    /// </summary>
    public void SetFullscreen(bool isFullscreen)
    {
        Data.IsFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Data.ResolutionIndex = resolutionIndex;
        Resolution res = AvailableResolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Data.IsFullscreen);
    }
}
