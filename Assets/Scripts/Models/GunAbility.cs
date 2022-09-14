using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class GunAbility : IAbility
{
    private readonly Rigidbody2D _viewPrefab;
    private readonly float _speed;
    private readonly float _strength;
    public GunAbility(GameObject view, float speed, float strength)
    {
        _viewPrefab = view.GetComponent<Rigidbody2D>();
        _speed = speed;
        _strength = strength;
    }
    public void Apply(IAbilityActivator activator)
    {
        var projectile = GameObject.Instantiate(_viewPrefab);
        projectile.AddForce((activator.GetViewObject().transform.right * _speed), ForceMode2D.Impulse);
        Object.Destroy(projectile, 2.0f);
    }
}
