using System;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityCollectionView
{
    event EventHandler<IItem> UseRequested;
    void Display(IReadOnlyList<IItem> abilityItems);
}
