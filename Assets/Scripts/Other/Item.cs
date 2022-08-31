using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : IItem
{
    public int Id { get; set; }
    public ItemInfo Info { get; set; }
}
