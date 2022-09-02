using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShedController : BaseController, IShedController
{
    private readonly Car _car;
    private readonly ProfilePlayer _player;
    private IInventoryModel _inventoryModel;
    private IRepository<int, IItem> _itemsRepository;
    private IRepository<int, IUpgradeHandler> _upgradeRepository;
    private IInventoryController _inventoryController;
    private IInventorySelector _inventorySelector;
    public ShedController(IRepository<int, IItem> itemRepository,
                          IRepository<int, IUpgradeHandler> upgradeRepository,
                          ProfilePlayer player,
                          IInventoryController inventory,
                          IInventoryModel model,
                          IInventorySelector selector)
    {
        _player = player;
        _car = player.CurrentCar;
        _itemsRepository = itemRepository;
        _upgradeRepository = upgradeRepository;
        _inventoryController = inventory;
        _inventoryModel = model;
        _inventorySelector = selector;
    }
    public void Enter()
    {
        _car.Restore();
        _inventoryController.ShowInventory(_inventorySelector._model.GetEquippedItems());
    }

    public void Exit()
    {
        UpgradeCarWithEquippedItems(_inventoryModel, _car);
        _inventoryController.ShowInventory(_inventorySelector._model.GetEquippedItems());
    }
    private void UpgradeCarWithEquippedItems(IInventoryModel inventoryModel, Car car)
    {
        foreach (var item in inventoryModel.GetEquippedItems())
        {
            if (_upgradeRepository.Items.TryGetValue(item.Id, out var handler))
            {
                handler.Upgrade(_car);
            }
        }
    }
}