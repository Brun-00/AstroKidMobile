using UnityEngine;
using static PlayerScript;

public class PowerUPSpeed : PowerUPBase
{
    public float amountToSpeed;

    protected override void StartPowerUp()
    {
        // Apply the speed power-up with its configured duration and amount.
        PlayerScript.Instance.ApplyPowerUp(
            PowerUpType.Speed,
            duration,
            amountToSpeed
        );
    }
}