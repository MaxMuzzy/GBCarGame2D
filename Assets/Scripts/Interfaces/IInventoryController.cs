using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryController
{
    void ShowInventory(IReadOnlyList<IItem> items);
}
