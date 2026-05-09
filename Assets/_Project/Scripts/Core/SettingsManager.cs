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

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadAndApplySettings();
    }

    private void LoadAndApplySettings()
    {
        Data = SaveSystem.Load<SettingsData>(SETTINGS_FILE_NAME);
        ApplyAllSettings();
    }

    public void SaveSettings()
    {
        SaveSystem.Save(Data, SETTINGS_FILE_NAME);
    }

    public void ApplyAllSettings()
    {
        // Video Logic
        Screen.fullScreen = Data.IsFullscreen;
        QualitySettings.SetQualityLevel(Data.QualityIndex);

        // Audio Logic
        SetMasterVolume(Data.MasterVolume);
        SetMusicVolume(Data.MusicVolume);
        SetSFXVolume(Data.SFXVolume);
    }

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
}
