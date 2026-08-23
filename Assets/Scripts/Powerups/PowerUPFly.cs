using UnityEngine;
using static PlayerScript;

public class PowerUPFly : PowerUPBase
{
    protected override void StartPowerUp()
    {
        PlayerScript.Instance.ApplyPowerUp(PowerUpType.Fly, duration);
    }
}
