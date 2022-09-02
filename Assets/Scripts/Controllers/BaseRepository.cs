using System.Collections.Generic;

public abstract class BaseRepository<TValue, TConfig> : BaseController, IRepository<int, TValue> where TConfig : IConfig
{
    public IReadOnlyDictionary<int, TValue> Items => _items;

    private Dictionary<int, TValue> _items;

    public BaseRepository(List<TConfig> configs)
    {
        PopulateItems(configs);
    }

    private void PopulateItems(List<TConfig> cfgs)
    {
        _items = new Dictionary<int, TValue>();
        foreach (var config in cfgs)
        {
            if (_items.ContainsKey(config.Id))
                continue;

            _items.Add(config.Id, CreateItem(config));
        }
    }

    protected abstract TValue CreateItem(TConfig config);

    protected override void OnDispose()
    {
        _items.Clear();
        base.OnDispose();
    }
}
