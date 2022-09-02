using UnityEngine;

public class CfgSource<TConfig> : ScriptableObject where TConfig : class, IConfig
{
    [SerializeField]
    private TConfig[] _cfgs;
    public TConfig[] Cfgs => _cfgs;
}
