using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRepository<Tkey, TValue>
{
    IReadOnlyDictionary<Tkey, TValue> Items { get; }
}
