using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIPanel _initialPanel;
    private Stack<UIPanel> _history = new Stack<UIPanel>();

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        if(_initialPanel != null) OpenPanel(_initialPanel);
    }

    /// <summary>
    /// Hides the current panel, adds the new panel to the stack, and shows it.
    /// </summary>
    public void OpenPanel(UIPanel panel)
    {
        if (_history.Count > 0)
        {
            _history.Peek().Hide();
        }

        _history.Push(panel);
        panel.Show();
    }

    /// <summary>
    /// Closes the active panel and restores the previous panel in the stack.
    /// </summary>
    public void GoBack()
    {
        if (_history.Count <= 1) return; // Retains the root panel

        UIPanel current = _history.Pop();
        current.Hide();

        UIPanel previous = _history.Peek();
        previous.Show();
    }
}