using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryView
{
    void Display(IReadOnlyList<IItem> items);
}
