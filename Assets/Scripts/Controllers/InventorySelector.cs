using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelector : BaseController, IInventorySelector
{
    public IInventoryModel _model { get; }
    private readonly IRepository<int, IItem> _repository;
    private readonly ProfilePlayer _player;
    private readonly InventorySelectorView _view;
    public InventorySelector(IRepository<int, IItem> repository, IInventoryModel model, ProfilePlayer player, InventorySelectorView view)
    {
        _model = model;
        _repository = repository;
        _player = player;
        _view = view;
        _view.Init(EquipFastTire, EquipSlowTire, EquipWeapon, ClearInventory);
    }
    public void EquipFastTire()
    {
        // 1 id быстрой шины
        if (_repository.Items.TryGetValue(1, out var item))
            _model.EquipItem(item);
        else
            Debug.Log("Couldn't find fast tire item");
    }
    public void EquipSlowTire()
    {
        // 2 id медленной шины
        if (_repository.Items.TryGetValue(2, out var item))
            _model.EquipItem(item);
        else
            Debug.Log("Couldn't find slow tire item");
    }
    public void EquipWeapon()
    {
        // 123 id оружия
        if (_repository.Items.TryGetValue(123, out var item))
        {
            _player.CanBomb = true;
            _model.EquipItem(item);
        }
        else
            Debug.Log("Couldn't find weapon item");
    }
    public void ClearInventory()
    {
        _model.Clear();
        _player.CanBomb = false;
    }
    protected override void OnDispose()
    {
        ClearInventory();
        base.OnDispose();
    }
}
