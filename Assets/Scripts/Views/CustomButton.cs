using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
public class CustomButton : Button
{
    public new static string Transition => nameof(_animationButtonType);
    public static string CurveEase => nameof(_curveEase);
    public static string Duration => nameof(_duration);
    public static string Strength => nameof(_strength);
    [SerializeField]
    private TransitionType _animationButtonType = TransitionType.None;
    [SerializeField]
    private Ease _curveEase = Ease.Linear;
    [SerializeField]
    private float _duration = 0.6f;
    [SerializeField]
    private float _strength = 30.0f;

    private Tween _activeTween;
    protected override void Awake()
    {
        base.Awake();
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        ActivateAnimation();
    }
    private void ActivateAnimation()
    {
        _activeTween.Rewind();
        _activeTween?.Kill();
        _activeTween = null;
        switch (_animationButtonType)
        {
            case TransitionType.Position:
                _activeTween = (transform as RectTransform).DOShakeAnchorPos(_duration, Vector2.one * _strength).SetEase(_curveEase);
                break;
            case TransitionType.Rotation:
                _activeTween = (transform as RectTransform).DOShakeRotation(_duration, Vector3.forward * _strength).SetEase(_curveEase);
                break;
            case TransitionType.Scale:
                _activeTween = (transform as RectTransform).DOShakeScale(_duration, _strength, 4).SetEase(_curveEase);
                break;

        }
    }
}
public enum TransitionType
{
    None,
    Position,
    Rotation,
    Scale
}

