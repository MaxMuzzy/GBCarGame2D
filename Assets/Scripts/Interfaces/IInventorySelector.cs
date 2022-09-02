using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventorySelector
{
    IInventoryModel _model { get; }
    void ClearInventory();
}
