using UnityEngine;
using static PlayerScript;

public class PowerUPFly : PowerUPBase
{
    protected override void StartPowerUp()
    {
        // Enable the fly power-up for its configured duration.
        PlayerScript.Instance.ApplyPowerUp(
            PowerUpType.Fly,
            duration
        );
    }
}