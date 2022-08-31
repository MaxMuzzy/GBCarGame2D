using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : IInventoryView
{
    public void Display(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
            Debug.Log($"Item ID: {item.Id}. Item title: {item.Info.Title}");
    }
}
