using UnityEngine;
using DG.Tweening;

public class PowerUPBase : ItemCollectableBase
{
    private Transform player;

    [Header("Power Up")]
    public float duration;

    private void Awake()
    {
        // Find the player that will receive the power-up effect.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void OnCollect()
    {
        // Run the base collection effects before applying the power-up.
        base.OnCollect();

        // Play a quick scale animation to highlight the collection.
        player.transform
            .DOScale(1.2f, .2f)
            .SetEase(Ease.OutBack)
            .SetLoops(2, LoopType.Yoyo);

        StartPowerUp();
    }

    protected virtual void StartPowerUp()
    {
        // Schedule the power-up to end after its duration.
        Invoke(nameof(EndPowerUp), duration);
    }

    protected virtual void EndPowerUp()
    {
    }
}