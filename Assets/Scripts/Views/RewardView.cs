using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RewardView : MonoBehaviour
{
    [Header("Time settings")]
    [SerializeField]
    public int TimeDailyCooldown = 86400;
    [SerializeField]
    public int TimeDailyDeadline = 172800;
    [SerializeField]
    public int TimeWeeklyCooldown = 86400;
    [SerializeField]
    public int TimeWeeklyDeadline = 172800;
    [Space]
    [Header("RewardSettings")]
    public List<Reward> DailyRewards;
    public List<Reward> WeeklyRewards;
    [Header("UI")]
    [SerializeField]
    public Slider RewardDailyTimer;
    [SerializeField]
    public Slider RewardWeeklyTimer;
    [SerializeField]
    public Transform DailySlotsParent;
    [SerializeField]
    public Transform WeeklySlotsParent;
    [SerializeField]
    public SlotRewardView SlotPrefab;
    [SerializeField]
    public Button ResetButton;
    [SerializeField]
    public Button GetRewardButton;
    [SerializeField]
    public Button GetWeeklyRewardButton;
    [SerializeField]
    public Button ExitButton;

    public void Init(UnityAction exit)
    {
        ExitButton.onClick.AddListener(exit);
    }
    private void OnDestroy()
    {
        GetRewardButton.onClick.RemoveAllListeners();
        ResetButton.onClick.RemoveAllListeners();
        GetWeeklyRewardButton.onClick.RemoveAllListeners();
    }
}
