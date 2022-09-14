using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class PopupView : MonoBehaviour
{
    [SerializeField] private Button _buttonClosePopup;
    [SerializeField] private RectTransform _windowContainer;
    [SerializeField] private Image _background;
    [SerializeField] private float _duration = 0.3f;
    private void Awake()
    {
        _background.color = new Color(0, 0, 0, 0);
        _buttonClosePopup.onClick.AddListener(HidePopup);
    }
    private void OnDestroy()
    {
        _buttonClosePopup.onClick.RemoveAllListeners();
    }
    public void ShowPopup()
    {
        gameObject.SetActive(true);
        this._windowContainer.localScale = Vector3.zero;
        var sequence = DOTween.Sequence();
        sequence.Append(_windowContainer.DOScale(Vector3.one, _duration));
        sequence.Insert(0, _background.DOColor(new Color(0, 0, 0, 0.4f), _duration));
    }
    public void HidePopup()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(_windowContainer.DOScale(Vector3.zero, _duration));
        sequence.Insert(0, _background.DOColor(new Color(0, 0, 0, 0f), _duration));
    }
}

