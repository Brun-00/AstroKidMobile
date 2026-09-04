using UnityEngine;
using static PlayerScript;

public class PowerUPInvincible : PowerUPBase
{
    protected override void StartPowerUp()
    {
        // Apply the invincibility power-up for its configured duration.
        PlayerScript.Instance.ApplyPowerUp(
            PowerUpType.Invincible,
            duration
        );
    }
}