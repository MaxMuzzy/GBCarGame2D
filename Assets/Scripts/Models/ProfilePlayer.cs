using Profile;
using Shop;
using Tools;

public class ProfilePlayer
{
    public ProfilePlayer(float speedCar, IShop shop)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        CurrentCar.SetSpeed(speedCar);
        Shop = shop;
    }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }
    public bool CanBomb { get; set; }
    public IShop Shop { get; private set; }
}

