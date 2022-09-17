using Tools;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public abstract class BaseInputView : MonoBehaviour
{
    private SubscriptionProperty<float> _leftMove;
    private SubscriptionProperty<float> _rightMove;
    protected float _speed;
    public bool _isMovingRight;
    public bool _isMovingLeft;
    public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;
    }

    protected void OnLeftMove(float value)
    {
        _leftMove.Value = value;
        _isMovingLeft = true;
        _isMovingRight = false;
    }

    protected void OnRightMove(float value)
    {
        _rightMove.Value = value;
        _isMovingRight = true;
        _isMovingLeft = false;
    }
}

