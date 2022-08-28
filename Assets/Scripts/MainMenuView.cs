using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonPurchase;

    public void Init(UnityAction startGame, UnityAction onPurchase)
    {
        _buttonStart.onClick.AddListener(startGame);
        _buttonPurchase.onClick.AddListener(onPurchase);
    }

    protected void OnDestroy()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonPurchase.onClick.RemoveAllListeners();
    }
}