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
    //private GameObject _car;
    //private List<Transform> _children = new List<Transform>();
    //private float _limit = 720f;
    //private Tween _activeTween;
    public virtual void Init(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, float speed)
    {
        _leftMove = leftMove;
        _rightMove = rightMove;
        _speed = speed;
        //_car = car;
        /*foreach (var child in _car.transform.GetComponentsInChildren<Transform>())
        {
            _children.Add(child);
        }
        _children.Remove(_car.transform);*/
    }

    protected void OnLeftMove(float value)
    {
        _leftMove.Value = value;
        _isMovingLeft = true;
        _isMovingRight = false;
        /*foreach (var child in _children)
        {
            child.GetComponent<SpriteRenderer>().DOColor(Color.red, 1f);
            //child.DORotate(-Vector3.forward * _speed, 0.5f).SetLoops(-1);
        }*/
    }

    protected void OnRightMove(float value)
    {
        _rightMove.Value = value;
        _isMovingRight = true;
        _isMovingLeft = false;
        /*foreach (var child in _children)
        {
            child.GetComponent<SpriteRenderer>().DOColor(Color.green, 1f);
            //child.DORotate(Vector3.forward * _speed, 0.5f).SetLoops(-1);
        }*/
    }
    /*protected void KillTween()
    {
        _activeTween.Rewind();
        _activeTween?.Kill();
        _activeTween = null;
    }*/
}

