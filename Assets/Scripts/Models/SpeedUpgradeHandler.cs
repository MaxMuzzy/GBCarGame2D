using UnityEngine;

public class SpeedUpgradeHandler : IUpgradeHandler
{
    private readonly float _newSpeed;

    public SpeedUpgradeHandler(float newSpeed)
    {
        _newSpeed = newSpeed;
    }

    public void Upgrade(IUpgradeableCar car)
    {
        car.SetSpeed(_newSpeed);
    }
}
