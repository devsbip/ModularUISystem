using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    [Header("Anim Settings")]
    [SerializeField] private float _fadeDuration = .3f;
    [SerializeField] private Ease _easeType = Ease.OutQuad;

    [Header("Gamepad/Keyboard Nav")]
    [SerializeField] protected GameObject _firstSelectedElement;

    protected CanvasGroup _canvasGroup;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        HideInstant();
    }

    /// <summary>
    /// Logic to show the panel. Can be overridden for custom Tween Animations.
    /// </summary>
    public virtual void Show()
    {
        gameObject.SetActive(true);
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        _canvasGroup.DOKill();

        _canvasGroup.DOFade(1f, _fadeDuration)
                    .SetEase(_easeType)
                    .SetUpdate(true);
        
        SetFirstSelected();
    }

    /// <summary>
    /// Logic to hide the panel.
    /// </summary>
    public virtual void Hide()
    {
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

        _canvasGroup.DOKill();
        _canvasGroup.DOFade(0f, _fadeDuration)
                    .SetEase(_easeType)
                    .SetUpdate(true)
                    .OnComplete(() => gameObject.SetActive(false));
    }   

    /// <summary>
    /// Instantly hides the panel without any animation.
    /// </summary>
    public void HideInstant()
    {
        _canvasGroup.DOKill();
        _canvasGroup.alpha = 0f;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Clears the current selection and highlights the designated first element.
    /// </summary>
    private void SetFirstSelected()
    {
        if (_firstSelectedElement == null) return;

        // Clear current selection first to force the UI to refresh its visual state
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(_firstSelectedElement);
    }
}