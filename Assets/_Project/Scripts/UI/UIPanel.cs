using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UIPanel : MonoBehaviour
{
    [Header("Anim Settings")]
    [SerializeField] private float _fadeDuration = .3f;
    [SerializeField] private Ease _easeType = Ease.OutQuad;
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
}