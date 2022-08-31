using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeHandlerRepository : BaseController
{
    public IReadOnlyDictionary<int, IUpgradeHandler> UpgradeItems => _upgradeItems;

    private Dictionary<int, IUpgradeHandler> _upgradeItems = new Dictionary<int, IUpgradeHandler>();

    public UpgradeHandlerRepository(IReadOnlyList<UpgradeItemCfg> configs)
    {
        PopulateItems(ref _upgradeItems, configs);
    }

    private void PopulateItems(ref Dictionary<int, IUpgradeHandler> upgradeItems, IReadOnlyList<UpgradeItemCfg> cfgs)
    {
        foreach (var config in cfgs)
        {
            upgradeItems[config.Id] = CreateHandler(config);
        }
    }

    private IUpgradeHandler CreateHandler(UpgradeItemCfg cfg)
    {
        switch (cfg.UpgradeType)
        {
            case UpgradeType.None:
                return new StubUpgradeHandler();
            case UpgradeType.Speed:
                return new SpeedUpgradeHandler(cfg.ValueUpgrade);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
