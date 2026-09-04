using UnityEngine;
using static PlayerScript;

public class PowerUPGather : PowerUPBase
{
    protected override void StartPowerUp()
    {
        // Enable the gather power-up for its configured duration.
        PlayerScript.Instance.ApplyPowerUp(
            PowerUpType.Gather,
            duration
        );
    }
}