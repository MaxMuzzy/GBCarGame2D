using Profile;
using Shop;
using System;
using Tools;

public class ProfilePlayer
{
    private const string LastTimeDailyKey = "LastDailyRewardTime";
    private const string ActiveDailySlotKey = "ActiveDailySlot";
    private const string LastTimeWeeklyKey = "LastWeekRewardTime";
    private const string ActiveWeeklySlotKey = "ActiveWeekSlot";
    private const string WoodKey = "Wood";
    private const string DiamondKey = "Diamond";
    public ProfilePlayer(float speedCar, IShop shop)
    {
        CurrentState = new SubscriptionProperty<GameState>();
        CurrentCar = new Car(speedCar);
        CurrentCar.SetSpeed(speedCar);
        Shop = shop;

        CurrentDailyActiveSlot = new IntSubscriptionProperty(ActiveDailySlotKey);
        CurrentWeeklyActiveSlot = new IntSubscriptionProperty(ActiveWeeklySlotKey);

        LastDailyRewardTime = new DateTimeSubscriptionProperty(LastTimeDailyKey);
        LastWeeklyRewardTime = new DateTimeSubscriptionProperty(LastTimeWeeklyKey);
        Wood = new IntSubscriptionProperty(WoodKey);
        Diamond = new IntSubscriptionProperty(DiamondKey);
    }

    public SubscriptionProperty<int> CurrentDailyActiveSlot { get; }
    public SubscriptionProperty<int> CurrentWeeklyActiveSlot { get; }
    public SubscriptionProperty<DateTime?> LastDailyRewardTime { get; }
    public SubscriptionProperty<DateTime?> LastWeeklyRewardTime { get; }

    public SubscriptionProperty<int> Wood { get; }
    public SubscriptionProperty<int> Diamond { get; }

    public SubscriptionProperty<GameState> CurrentState { get; }

    public Car CurrentCar { get; }
    public bool CanBomb { get; set; }
    public IShop Shop { get; private set; }
}

