using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class GunAbility : IAbility
{
    private readonly Rigidbody2D _viewPrefab;
    private readonly float _speed;

    public GunAbility(GameObject view, float speed)
    {
        _viewPrefab = view.GetComponent<Rigidbody2D>();
        _speed = speed;
    }
    public void Apply(IAbilityActivator activator)
    {
        var projectile = Object.Instantiate(_viewPrefab);
        projectile.AddForce((activator.GetViewObject().transform.right * _speed), ForceMode2D.Impulse);
        Object.Destroy(projectile, 2.0f);
    }
}
