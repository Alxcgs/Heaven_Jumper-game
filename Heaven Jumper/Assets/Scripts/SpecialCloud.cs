using UnityEngine;
using DG.Tweening;

public class SpecialCloud : Platform
{
    [Header("Pulse Settings")]
    [SerializeField] private float pulseScale = 1.3f; // Збільшений масштаб
    [SerializeField] private float pulseDuration = 0.6f; // Швидша анімація
    [SerializeField] private Color pulseColor = new Color(0.7f, 0.7f, 0.7f); // Світліший колір при пульсації

    private SpriteRenderer _renderer;
    private Color _originalColor;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _originalColor = _renderer.color;
        forceJump = 10f;
        AnimatePulse();
    }

    private void AnimatePulse()
    {
        // Анімація масштабу
        transform.DOScale(pulseScale, pulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);

        // Анімація кольору
        _renderer.DOColor(pulseColor, pulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
}