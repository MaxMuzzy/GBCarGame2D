public class Car : IUpgradeableCar
{
    public float Speed { get; private set; }

    private float _baseSpeed;

    public Car(float speed)
    {
        _baseSpeed = speed;
        Restore();
    }

    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public void Restore()
    {
        Speed = _baseSpeed;
    }
}