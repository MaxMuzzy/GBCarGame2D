using System;
using UnityEngine;

public class DateTimeSubscriptionProperty : PrefsSubscriptionProperty<DateTime?>
{
    public DateTimeSubscriptionProperty(string key) : base(key) {}

    protected override DateTime? GetValue()
    {
        var data = PlayerPrefs.GetString(Key);
        if (string.IsNullOrEmpty(data))
            return null;
        return DateTime.Parse(data);
    }

    protected override void SetValue(DateTime? value)
    {
        if (value != null)
            PlayerPrefs.SetString(Key, value.ToString());
        else
            PlayerPrefs.DeleteKey(Key);
    }
}

