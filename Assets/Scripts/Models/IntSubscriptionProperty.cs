using UnityEngine;

public class IntSubscriptionProperty : PrefsSubscriptionProperty<int>
{
    public IntSubscriptionProperty(string key) : base(key) {}
    protected override int GetValue()
    {
        return PlayerPrefs.GetInt(Key, 0);
    }

    protected override void SetValue(int value)
    {
        PlayerPrefs.SetInt(Key, value);
    }
}

