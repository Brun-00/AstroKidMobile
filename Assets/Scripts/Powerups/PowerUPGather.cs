using UnityEngine;
using static PlayerScript;

public class PowerUPGather : PowerUPBase
{
    protected override void StartPowerUp()
    {
        PlayerScript.Instance.ApplyPowerUp(PowerUpType.Gather, duration);
    }
}
