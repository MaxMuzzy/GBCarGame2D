using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventorySelectorView : MonoBehaviour
{
    [SerializeField] private Button _fastTireButton;
    [SerializeField] private Button _slowTireButton;
    [SerializeField] private Button _weaponButton;
    [SerializeField] private Button _clearButton;

    public void Init(UnityAction fastTire, UnityAction slowTire, UnityAction weapon, UnityAction clear)
    {
        _fastTireButton.onClick.AddListener(fastTire);
        _slowTireButton.onClick.AddListener(slowTire);
        _weaponButton.onClick.AddListener(weapon);
        _clearButton.onClick.AddListener(clear);
    }

    protected void OnDestroy()
    {
        _fastTireButton.onClick.RemoveAllListeners();
        _slowTireButton.onClick.RemoveAllListeners();
        _weaponButton.onClick.RemoveAllListeners();
        _clearButton.onClick.RemoveAllListeners();
    }
}
