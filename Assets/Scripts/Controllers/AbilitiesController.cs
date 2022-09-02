using System;
using UnityEngine;
using Tools;

public class AbilitiesController : BaseController
{
    private readonly IAbilityActivator _activator;
    private readonly IRepository<int, IAbility> _repository; 
    private readonly AbilitiesView _view;
    private readonly ProfilePlayer _player;
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Abilities" };

    public AbilitiesController(IAbilityActivator activator, IRepository<int, IAbility> repository, ProfilePlayer player)
    {
        _activator = activator;
        _repository = repository;
        _player = player;
        _view = ResourceLoader.LoadAndInstantiate<AbilitiesView>(_viewPath, null);
        _view.Init(BombAbility);
    }
    private void BombAbility()
    {
        if (_repository.Items.TryGetValue(123, out var ability) && _player.CanBomb)
            ability.Apply(_activator);
        else
            Debug.Log("Couldn't find bomb ability item!");
    }
}