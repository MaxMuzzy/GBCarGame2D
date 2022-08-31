using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityItemCfg", menuName = "AbilityItemCfg")]
public class AbilityItemCfg : ScriptableObject
{
    public ItemCfg ItemCfg;
    public GameObject View;
    public AbilityType Type;
    public float Value;
    public int Id => ItemCfg.Id;
}
public enum AbilityType
{
    None,
    Gun
}
