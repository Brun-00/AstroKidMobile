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
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        rectTransform.DOAnchorPosY(
            originalPosition.y + moveDistance,
            duration
        )
        .SetEase(ease)
        .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        rectTransform.DOKill();
    }
}