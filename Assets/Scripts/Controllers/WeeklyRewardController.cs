using JoostenProductions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeklyRewardController : BaseController
{
    private readonly ProfilePlayer _profilePlayer;
    private readonly CurrencyWindow _currency;
    private readonly RewardView _view;
    private List<SlotRewardView> _slots;

    private bool _rewardReceived = false;

    public WeeklyRewardController(ProfilePlayer profilePlayer, CurrencyWindow currency, RewardView view)
    {
        _profilePlayer = profilePlayer;
        _currency = currency;
        _view = view;

        InitSlots();
        RefreshUi();
        UpdateManager.SubscribeToUpdate(Update);
        SubscribeButtons();
        SubscribeResources();

        AddGameObjects(_currency.gameObject);
        AddGameObjects(_view.gameObject);
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
        if (_profilePlayer.LastWeeklyRewardTime.Value.HasValue)
        {
            var timeSpan = DateTime.UtcNow - _profilePlayer.LastWeeklyRewardTime.Value.Value;
            if (timeSpan.Seconds > _view.TimeWeeklyDeadline)
            {
                _profilePlayer.LastWeeklyRewardTime.Value = null;
                _profilePlayer.CurrentWeeklyActiveSlot.Value = 0;
            }
            else if (timeSpan.Seconds < _view.TimeWeeklyCooldown)
            {
                _rewardReceived = true;
            }
        }
    }

    private void RefreshUi()
    {
        _view.GetWeeklyRewardButton.interactable = !_rewardReceived;

        for (var i = 0; i < _view.WeeklyRewards.Count; i++)
        {
            _slots[i].SetData(_view.WeeklyRewards[i], i + 1, i <= _profilePlayer.CurrentWeeklyActiveSlot.Value);
        }

        DateTime nextWeeklyBonusTime =
            !_profilePlayer.LastWeeklyRewardTime.Value.HasValue
                ? DateTime.MinValue
                : _profilePlayer.LastWeeklyRewardTime.Value.Value.AddSeconds(_view.TimeWeeklyCooldown);
        var delta = nextWeeklyBonusTime - DateTime.UtcNow;
        if (delta.TotalSeconds < 0)
            delta = new TimeSpan(0);

        _view.RewardWeeklyTimer.value = (float)delta.Seconds / (float)_view.TimeWeeklyCooldown;
    }

    private void InitSlots()
    {
        _slots = new List<SlotRewardView>();
        for (int i = 0; i < _view.WeeklyRewards.Count; i++)
        {
            var reward = _view.WeeklyRewards[i];
            var slotInstance = GameObject.Instantiate(_view.SlotPrefab, _view.WeeklySlotsParent, false);
            slotInstance.SetData(reward, i + 1, false);
            _slots.Add(slotInstance);
        }
    }

    private void SubscribeButtons()
    {
        _view.GetWeeklyRewardButton.onClick.AddListener(ClaimReward);
        _view.ResetButton.onClick.AddListener(ResetReward);
    }

    private void ResetReward()
    {
        _profilePlayer.LastWeeklyRewardTime.Value = null;
        _profilePlayer.CurrentWeeklyActiveSlot.Value = 0;
    }

    private void ClaimReward()
    {
        if (_rewardReceived)
            return;
        if (_profilePlayer.CurrentWeeklyActiveSlot.Value > 2)
            _profilePlayer.CurrentWeeklyActiveSlot.Value = 0;
        var reward = _view.WeeklyRewards[_profilePlayer.CurrentWeeklyActiveSlot.Value];
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

        _profilePlayer.LastWeeklyRewardTime.Value = DateTime.UtcNow;
        _profilePlayer.CurrentWeeklyActiveSlot.Value = (_profilePlayer.CurrentWeeklyActiveSlot.Value + 1) % _view.WeeklyRewards.Count;
        RefreshRewardState();
    }
    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(Update);
        base.OnDispose();
    }
}