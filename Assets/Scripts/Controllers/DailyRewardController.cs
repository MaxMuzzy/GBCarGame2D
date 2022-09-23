using JoostenProductions;
using Profile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using Unity.Notifications.iOS;

public class DailyRewardController : BaseController
{
    private const string _androidNotifierId = "android_notifier_id";
    private const string _iOSNotifierId = "ios_notifier_id";

    private readonly ProfilePlayer _profilePlayer;
    private readonly CurrencyWindow _currency;
    private readonly RewardView _view;
    private List<SlotRewardView> _slots;

    private bool _rewardReceived = false;
    public DailyRewardController(ProfilePlayer profilePlayer, CurrencyWindow currency, RewardView view)
    {
        _profilePlayer = profilePlayer;
        _currency = currency;
        _view = view;
        _view.Init(Exit);
        InitSlots();
        RefreshUi();
        UpdateManager.SubscribeToUpdate(Update);        
        SubscribeButtons();
        SubscribeResources();

        AddGameObjects(_currency.gameObject);
        AddGameObjects(_view.gameObject);
    }

    private void Exit()
    {
        _profilePlayer.CurrentState.Value = GameState.Start;
    }

    private void Update()
    {
        RefreshRewardState();
        RefreshUi();
    }
    private void SubscribeResources()
    {
        _profilePlayer.Wood.SubscribeOnChange(UpdateCurrencyView);
        _profilePlayer.Diamond.SubscribeOnChange(UpdateCurrencyView);
    }

    private void UpdateCurrencyView(int obj)
    {
        _currency.RefreshText(_profilePlayer.Wood.Value, _profilePlayer.Diamond.Value);
    }

    private void RefreshRewardState()
    {
        _rewardReceived = false;
        if (_profilePlayer.LastDailyRewardTime.Value.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _profilePlayer.LastDailyRewardTime.Value.Value;
            if (timeSpan.Seconds > _view.TimeDailyDeadline)
            {
                _profilePlayer.LastDailyRewardTime.Value = null;
                _profilePlayer.CurrentDailyActiveSlot.Value = 0;
                CreateNotifications();
            }
            else if (timeSpan.Seconds < _view.TimeDailyCooldown)
            {
                _rewardReceived = true;
            }
        }
    }

    private void RefreshUi()
    {
        _view.GetRewardButton.interactable = !_rewardReceived;

        for (var i = 0; i < _view.DailyRewards.Count; i++)
        {
            _slots[i].SetData(_view.DailyRewards[i], i + 1, i <= _profilePlayer.CurrentDailyActiveSlot.Value);
        }

        DateTime nextDailyBonusTime =
            !_profilePlayer.LastDailyRewardTime.Value.HasValue
                ? DateTime.MinValue
                : _profilePlayer.LastDailyRewardTime.Value.Value.AddSeconds(_view.TimeDailyCooldown);
        var delta = nextDailyBonusTime - DateTime.UtcNow;
        if (delta.TotalSeconds < 0)
            delta = new TimeSpan(0);

        _view.RewardDailyTimer.value = (float)delta.Seconds / (float)_view.TimeDailyCooldown;
    }

    private void InitSlots()
    {
        _slots = new List<SlotRewardView>();
        for (int i = 0; i < _view.DailyRewards.Count; i++)
        {
            var reward = _view.DailyRewards[i];
            var slotInstance = GameObject.Instantiate(_view.SlotPrefab, _view.DailySlotsParent, false);
            slotInstance.SetData(reward, i + 1, false);
            _slots.Add(slotInstance);
        }
    }

    private void SubscribeButtons()
    {
        _view.GetRewardButton.onClick.AddListener(ClaimReward);
        _view.ResetButton.onClick.AddListener(ResetReward);
    }

    private void ResetReward()
    {
        _profilePlayer.LastDailyRewardTime.Value = null;
        _profilePlayer.CurrentDailyActiveSlot.Value = 0;
    }

    private void ClaimReward()
    {
        if (_rewardReceived)
            return;
        if (_profilePlayer.CurrentDailyActiveSlot.Value > 2)
            _profilePlayer.CurrentDailyActiveSlot.Value = 0;
        var reward = _view.DailyRewards[_profilePlayer.CurrentDailyActiveSlot.Value];
        switch (reward.Type)
        {
            case RewardType.None:
                break;
            case RewardType.Wood:
                _profilePlayer.Wood.Value += reward.Count;
                break;
            case RewardType.Diamond:
                _profilePlayer.Diamond.Value += reward.Count;
                break;
        }

        _profilePlayer.LastDailyRewardTime.Value = DateTime.UtcNow;
        _profilePlayer.CurrentDailyActiveSlot.Value = (_profilePlayer.CurrentDailyActiveSlot.Value + 1) % _view.DailyRewards.Count;
        RefreshRewardState();
    }
    private void CreateNotifications()
    {
        if (Application.platform == RuntimePlatform.Android)
            CreateAndroidNotification();
        if (Application.platform == RuntimePlatform.OSXPlayer)
            CreateIOSNotification();
    }

    private void CreateAndroidNotification()
    {
        var androidSettingChannel = new AndroidNotificationChannel
        {
            Id = _androidNotifierId,
            Name = "Game Notifier",
            Importance = Importance.High,
            CanBypassDnd = true,
            CanShowBadge = true,
            Description = "Your reward is ready to be collected!",
            EnableLights = true,
            EnableVibration = true,
            LockScreenVisibility = LockScreenVisibility.Public
        };
        AndroidNotificationCenter.RegisterNotificationChannel(androidSettingChannel);
        var androidSettingsNotification = new AndroidNotification
        {
            Title = "Get in here!",
            Color = Color.white,
            RepeatInterval = TimeSpan.FromDays(1)
        };
        AndroidNotificationCenter.SendNotification(androidSettingsNotification, _androidNotifierId);
    }
    private void CreateIOSNotification()
    {
        var iosSettingsNotification = new iOSNotification
        {
            Identifier = "android_notifier_id",
            Title = "Game Notifier",
            Subtitle = "Subtitle notifier",
            Body = "Your reward is ready to be collected!",
            Badge = 1,
            Data = "23/09/2022",
            ForegroundPresentationOption = PresentationOption.Alert | PresentationOption.Badge | PresentationOption.Sound
        };
        iOSNotificationCenter.ScheduleNotification(iosSettingsNotification);
    }

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(Update);
        base.OnDispose();
    }
}