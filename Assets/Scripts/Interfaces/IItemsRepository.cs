using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemsRepository
{
    IReadOnlyDictionary<int, IItem> Items { get; }
}
