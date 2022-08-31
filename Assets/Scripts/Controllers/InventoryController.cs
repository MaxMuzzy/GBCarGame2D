using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IItemsRepository _itemsRepository;
    private readonly IInventoryView _inventoryView;

    public InventoryController(IInventoryModel model, IItemsRepository repository, IInventoryView view)
    {
        _inventoryModel = model;
        _itemsRepository = repository;
        _inventoryView = view;
    }

    public void ShowInventory(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryView.Display(items);
    }
}
