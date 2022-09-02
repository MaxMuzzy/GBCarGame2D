using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UpgradeHandlerRepository : BaseRepository<IUpgradeHandler, UpgradeItemCfg>
{
    public UpgradeHandlerRepository(List<UpgradeItemCfg> configs) : base(configs) { }

    protected override IUpgradeHandler CreateItem(UpgradeItemCfg cfg)
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
