using Profile;
using Tools;
using UnityEngine;
using System.Collections.Generic;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;
    private InventorySelectorView _selectorView;

    private ShedController _shedController;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, OnPurchase);


        var trailController = new TrailController();
        AddController(trailController);

        _profilePlayer.Shop.OnSuccessPurchase.SubscribeOnChange(OnSuccessPurchase);
        _profilePlayer.Shop.OnFailedPurchase.SubscribeOnChange(OnFailedPurchase);

        _shedController = ConfigureShedController(placeForUi, _profilePlayer, _selectorView);
        _shedController.Enter();
    }

    // приходится делать новый метод тк менюшки не очистятся после перехода в игру
    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        _selectorView = objectView.GetComponent<InventorySelectorView>();
        return objectView.GetComponent<MainMenuView>();
    }

    private ShedController ConfigureShedController(Transform placeForUi, ProfilePlayer player, InventorySelectorView selectorView)
    {
        var itemCfgs = DataSourceLoader.LoadCfgs<ItemCfg>(new ResourcePath() { PathResource = "Configs/Sources/ItemsSource" });
        var upgradeCfgs = DataSourceLoader.LoadCfgs<UpgradeItemCfg>(new ResourcePath() { PathResource = "Configs/Sources/UpgradesSource" });
        var itemsRepository = new ItemsRepository(itemCfgs);
        AddController(itemsRepository);
        var upgradeRepository = new UpgradeHandlerRepository(upgradeCfgs);
        AddController(upgradeRepository);
        var inventoryModel = new InventoryModel();
        var inventoryView = new InventoryView();
        var inventoryController = new InventoryController(inventoryModel, inventoryView);
        AddController(inventoryController);
        var selector = new InventorySelector(itemsRepository, inventoryModel, player, selectorView);
        return new ShedController(itemsRepository, upgradeRepository, player, inventoryController, inventoryModel, selector);
    }

    private void StartGame()
    {
        _shedController.Exit();
        _profilePlayer.CurrentState.Value = GameState.Game;
    }
    private void OnPurchase()
    {
        _profilePlayer.Shop.Buy("com.mycompany.cargame.SmallCoins");
    }
    private void OnSuccessPurchase()
    {
        Debug.Log("Purchase successful!");
    }
    private void OnFailedPurchase()
    {
        Debug.Log("Purchase failed!");
    }

    protected override void OnDispose()
    {
        _profilePlayer.Shop.OnSuccessPurchase.SubscribeOnChange(OnSuccessPurchase);
        _profilePlayer.Shop.OnFailedPurchase.SubscribeOnChange(OnFailedPurchase);
        base.OnDispose();
    }
}

