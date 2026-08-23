using UnityEngine;
using static PlayerScript;

public class PowerUPInvincible : PowerUPBase
{
    protected override void StartPowerUp()
    {
        PlayerScript.Instance.ApplyPowerUp(PowerUpType.Invincible, duration);
    }
}