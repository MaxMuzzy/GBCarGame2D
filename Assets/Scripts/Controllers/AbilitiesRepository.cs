using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class AbilitiesRepository : BaseRepository<IAbility, AbilityItemCfg>
{
    public AbilitiesRepository(List<AbilityItemCfg> configs) : base(configs) { }

    protected override IAbility CreateItem(AbilityItemCfg config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return new StubAbility();
            case AbilityType.Gun:
                return new GunAbility(config.View, config.Value, config.Duration);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
