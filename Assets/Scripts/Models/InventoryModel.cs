using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel : IInventoryModel
{
    public readonly List<IItem> _items = new List<IItem>();

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _items;
    }

    public void EquipItem(IItem item)
    {
        if (_items.Contains(item))
            return;

        _items.Add(item);
    }

    public void UnEquipItem(IItem item)
    {
        _items.Remove(item);
    }
    public void Clear()
    {
        if (_items != null)
            _items.Clear();
    }
}
