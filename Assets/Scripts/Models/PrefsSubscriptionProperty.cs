using Tools;

public abstract class PrefsSubscriptionProperty<T> : SubscriptionProperty<T>
{
    protected string Key { get; private set; }
    protected PrefsSubscriptionProperty(string key)
    {
        Key = key;
        this.Value = GetValue();
        this.SubscribeOnChange(SetValue);
    }
    protected abstract T GetValue();
    protected abstract void SetValue(T value);
}

