using UnityEngine;
using DG.Tweening;

public class GrowObstacle : MonoBehaviour
{
    void Start()
    {
        transform.DOScale(new Vector3(3f, 1.5f, 1f), 3f)
            .SetEase(Ease.OutBack)
            .SetLoops(-1, LoopType.Yoyo);
    }

}
