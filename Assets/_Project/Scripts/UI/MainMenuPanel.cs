using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [Header("MainMenu Buttons")]
    [SerializeField] private Button _playBtn;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _quitBtn;

    [Header("Nav References")]
    [SerializeField] private UIPanel _settingsPanel;

    void Start()
    {
        _playBtn.onClick.AddListener(OnPlayClicked);
        _settingsBtn.onClick.AddListener(OnSettingsClicked);
        _quitBtn.onClick.AddListener(OnQuitClicked);
    }

    private void OnPlayClicked()
    {
        Debug.Log("Play Clicked - Starting Game Logic...");
    }

    private void OnSettingsClicked()
    {
        UIManager.Instance.OpenPanel(_settingsPanel);
    }

    private void OnQuitClicked()
    {
        Application.Quit();
    }
}
