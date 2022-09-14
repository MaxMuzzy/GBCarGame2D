﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityItemCfg", menuName = "AbilityItemCfg")]
public class AbilityItemCfg : ScriptableObject, IConfig
{
    public ItemCfg ItemCfg;
    public GameObject View;
    public AbilityType Type;
    public float Value;
    public float Duration;
    public float Strength;
    public int Id => ItemCfg.Id;
}
public enum AbilityType
{
    None,
    Gun
}
