using System.Collections.Generic;
using System.Linq;

public static class DataSourceLoader
{
    public static List<TConfig> LoadCfgs<TConfig>(ResourcePath path) where TConfig : class, IConfig
    {
        var cfgSource = ResourceLoader.LoadObject<CfgSource<TConfig>>(path);
        return cfgSource != null ? cfgSource.Cfgs.ToList() : new List<TConfig>();
    }
}
