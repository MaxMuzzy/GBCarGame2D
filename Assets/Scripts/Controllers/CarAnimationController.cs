using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JoostenProductions;
using DG.Tweening;

public class CarAnimationController : BaseController
{
    private GameObject _car;
    private List<Transform> _children = new List<Transform>();
    private readonly float _duration = 1f;
    private readonly float _limit = 720f;
    private BaseInputView _view;
    public CarAnimationController(GameObject car, BaseInputView view)
    {
        _car = car;
        _view = view;
        foreach(var child in _car.transform.GetComponentsInChildren<Transform>())
        {
            _children.Add(child);
        }
        _children.Remove(_car.transform);
        UpdateManager.SubscribeToUpdate(OnUpdate);
    }
    private void OnUpdate() 
    {
        if (_view._isMovingLeft && !_view._isMovingRight)
        {
            foreach (var child in _children)
            {
                child.GetComponent<SpriteRenderer>().DOColor(Color.red, _duration);
                child.DORotate(Vector3.forward * _limit, _duration).SetLoops(-1);
            }
        }
        else if (_view._isMovingRight && !_view._isMovingLeft)
        {
            foreach (var child in _children)
            {
                child.GetComponent<SpriteRenderer>().DOColor(Color.green, _duration);
                child.DORotate(Vector3.back * _limit, _duration).SetLoops(-1);
            }
        }
        else if (!_view._isMovingRight && !_view._isMovingLeft)
        {
            foreach (var child in _children)
            {
                child.GetComponent<SpriteRenderer>().DOColor(Color.yellow, _duration);
                child.DORotate(Vector3.zero, _duration);
            }
        }
    }

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        base.OnDispose();
    }

}
