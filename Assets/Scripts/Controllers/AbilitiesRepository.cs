using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AbilitiesRepository : BaseController, IAbilityRepository
{
    private readonly List<AbilityItemCfg> _abilities;
    public IReadOnlyDictionary<int, IAbility> AbilitiesMap => map;

    private Dictionary<int, IAbility> map;

    public AbilitiesRepository(List<AbilityItemCfg> abilities)
    {
        _abilities = abilities;
        PopulateAbilities(_abilities, ref map);
    }

    private void PopulateAbilities(List<AbilityItemCfg> abilities, ref Dictionary<int, IAbility> dictinary)
    {
        dictinary = new Dictionary<int, IAbility>();
        foreach (var config in _abilities)
        {
            dictinary[config.Id] = CreateAbility(config);
        }
    }

    private IAbility CreateAbility(AbilityItemCfg config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return new StubAbility();
            case AbilityType.Gun:
                return new GunAbility(config.View, config.Value);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
