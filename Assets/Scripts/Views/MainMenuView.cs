﻿using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonPurchase;
    [SerializeField] private Button _buttonOpenPopup;
    [SerializeField] private PopupView _popup;

    public void Init(UnityAction startGame, UnityAction onPurchase)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonPurchase.onClick.AddListener(onPurchase);
        _buttonOpenPopup.onClick.AddListener(_popup.ShowPopup);
        _popup.gameObject.SetActive(false);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonPurchase.onClick.RemoveAllListeners();
        _buttonOpenPopup.onClick.RemoveAllListeners();
    }
}