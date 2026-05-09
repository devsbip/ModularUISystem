using UnityEngine;

[System.Serializable]
public class SettingsData : MonoBehaviour
{
    // Audio Settings
    public float MasterVolume = .8f;
    public float MusicVolume = .8f;
    public float SFXVolume = .8f;

    // Video Settings
    public int QualityIndex = 2;
    public bool IsFullscreen = true;
    public int ResolutionIndex = 0;
}
