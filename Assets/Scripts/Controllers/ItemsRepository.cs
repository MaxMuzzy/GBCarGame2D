using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsRepository : BaseController, IItemsRepository
{
    public IReadOnlyDictionary<int, IItem> Items => _itemsMapById;

    private Dictionary<int, IItem> _itemsMapById = new Dictionary<int, IItem>();

    public ItemsRepository(List<ItemCfg> itemCfgs)
    {
        PopulateItems(itemCfgs);
    }

    protected override void OnDispose()
    {
        _itemsMapById.Clear();
    }

    private void PopulateItems(List<ItemCfg> cfgs)
    {
        foreach (var config in cfgs)
        {
            if (_itemsMapById.ContainsKey(config.Id))
                continue;

            _itemsMapById.Add(config.Id, CreateItem(config));
        }
    }

    private IItem CreateItem(ItemCfg itemCfg)
    {
        return new Item
        {
            Id = itemCfg.Id,
            Info = new ItemInfo { Title = itemCfg.Title }
        };
    }
}
