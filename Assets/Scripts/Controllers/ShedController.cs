using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShedController : BaseController, IShedController
{
    private readonly Car _car;
    private readonly List<ItemCfg> _itemCfgs;
    private readonly List<UpgradeItemCfg> _upgradeCfgs;
    private readonly ProfilePlayer _player;
    private IInventoryModel _inventoryModel;
    private IItemsRepository _itemsRepository;
    private IInventoryView _inventoryView;
    private IInventoryController _inventoryController;
    private InventorySelector _inventorySelector;
    private UpgradeHandlerRepository _upgradeRepository;
    public ShedController(Car car, List<ItemCfg> itemCfgs, List<UpgradeItemCfg> upgradeCfgs, ProfilePlayer player, Transform placeForUi, InventorySelectorView selectorView)
    {
        _player = player;
        _car = car;
        _itemCfgs = itemCfgs;
        _upgradeCfgs = upgradeCfgs;
        _inventoryModel = new InventoryModel();
        _itemsRepository = new ItemsRepository(itemCfgs);
        _inventoryView = new InventoryView();

        var inventory = new InventoryController(_inventoryModel, _itemsRepository, _inventoryView);
        AddController(inventory);
        _inventoryController = inventory;

        var upgrades = new UpgradeHandlerRepository(_upgradeCfgs);
        _upgradeRepository = upgrades;
        AddController(upgrades);

        var selector = new InventorySelector(_itemsRepository, _inventoryModel, _player, placeForUi, selectorView);
        _inventorySelector = selector;
        AddController(selector);
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
            if (_upgradeRepository.UpgradeItems.TryGetValue(item.Id, out var handler))
            {
                handler.Upgrade(_car);
            }
        }
    }
}