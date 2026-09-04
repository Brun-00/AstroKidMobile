using UnityEngine;
using DG.Tweening;

public class TitleSwoosh : MonoBehaviour
{
    [SerializeField] private float moveDistance = 25f;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private Ease ease = Ease.InOutSine;

    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        // Cache the UI transform and its starting position.
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    private void Start()
    {
        // Start the looping title animation.
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        // Move the title vertically and smoothly loop the motion.
        rectTransform
            .DOAnchorPosY(
                originalPosition.y + moveDistance,
                duration
            )
            .SetEase(ease)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        // Stop any active tween when the object is destroyed.
        rectTransform.DOKill();
    }
}