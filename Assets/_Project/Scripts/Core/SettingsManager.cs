using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadSettings();
    }

    /// <summary>
    /// Loads user preferences from persistent storage.
    /// </summary>
    private void LoadSettings()
    {
        // TODO: Implement JSON/PlayerPrefs loading logic
        Debug.Log("Settings Loaded");
    }

    /// <summary>
    /// Saves current user preferences to persistent storage.
    /// </summary>
    public void SaveSettings()
    {
        // TODO: Implement JSON/PlayerPrefs saving logic
        Debug.Log("Settings Saved");
    }

    /// <summary>
    /// Applies master volume changes to the AudioMixer.
    /// </summary>
    /// <param name="volume">Normalized volume value (0.0 to 1.0)</param>
    public void SetMasterVolume(float volume)
    {
        // TODO: Implement AudioMixer logic
    }

    /// <summary>
    /// Applies fullscreen state changes.
    /// </summary>
    /// <param name="isFullscreen">Target fullscreen state</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
