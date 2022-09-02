using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemsRepository : BaseRepository<IItem, ItemCfg>
{
    public ItemsRepository(List<ItemCfg> configs) : base(configs) {}

    protected override IItem CreateItem(ItemCfg itemCfg)
    {
        return new Item
        {
            Id = itemCfg.Id,
            Info = new ItemInfo { Title = itemCfg.Title }
        };
    }
}
