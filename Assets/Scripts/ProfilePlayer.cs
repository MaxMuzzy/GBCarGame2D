using Profile;
using Shop;
using Tools;

public class ProfilePlayer
{
    public ProfilePlayer(float speedCar, IShop shop)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        Shop = shop;
    }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }
    public IShop Shop { get; private set; }
}

