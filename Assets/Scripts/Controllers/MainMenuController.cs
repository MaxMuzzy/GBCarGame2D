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
    private readonly List<ItemCfg> _itemCfgs;
    private readonly List<UpgradeItemCfg> _upgradeCfgs;

    private ShedController _shedController;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer, List<ItemCfg> itemCfgs, List<UpgradeItemCfg> upgradeCfgs)
    {
        _profilePlayer = profilePlayer;
        _itemCfgs = itemCfgs;
        _upgradeCfgs = upgradeCfgs;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, OnPurchase);


        var trailController = new TrailController();
        AddController(trailController);

        _profilePlayer.Shop.OnSuccessPurchase.SubscribeOnChange(OnSuccessPurchase);
        _profilePlayer.Shop.OnFailedPurchase.SubscribeOnChange(OnFailedPurchase);

        _shedController = new ShedController(_profilePlayer.CurrentCar, _itemCfgs, _upgradeCfgs, _profilePlayer, placeForUi, _selectorView);
        _shedController.Enter();
    }


    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        _selectorView = objectView.GetComponent<InventorySelectorView>();
        return objectView.GetComponent<MainMenuView>();
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

