using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class InventorySelector : BaseController
{
    public readonly IInventoryModel _model;
    private readonly IItemsRepository _repository;
    private readonly List<IItem> _itemsToReturn;
    private readonly ProfilePlayer _player;
    private readonly Transform _placeForUi;
    private readonly InventorySelectorView _view;
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Selector" };
    public InventorySelector(IItemsRepository repository, IInventoryModel model, ProfilePlayer player, Transform placeForUi, InventorySelectorView view)
    {
        _model = model;
        _repository = repository;
        _player = player;
        _placeForUi = placeForUi;
        _view = view;
        _view.Init(EquipFastTire, EquipSlowTire, EquipWeapon, ClearInventory);
    }
    public void EquipFastTire()
    {
        if (_repository.Items.TryGetValue(1, out var item))
            _model.EquipItem(item);
        else
            Debug.Log("Couldn't find fast tire item");
    }
    public void EquipSlowTire()
    {
        if (_repository.Items.TryGetValue(2, out var item))
            _model.EquipItem(item);
        else
            Debug.Log("Couldn't find slow tire item");
    }
    public void EquipWeapon()
    {
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
