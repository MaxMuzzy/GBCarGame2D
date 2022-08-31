using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryModel
{
    IReadOnlyList<IItem> GetEquippedItems();
    void EquipItem(IItem item);
    void UnEquipItem(IItem item);
    void Clear();
}
