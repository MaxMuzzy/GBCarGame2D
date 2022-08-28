using Profile;
using Tools;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/mainMenu"};
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;

    public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, OnPurchase);

        var trailController = new TrailController();
        AddController(trailController);
        _profilePlayer.Shop.OnSuccessPurchase.SubscribeOnChange(OnSuccessPurchase);
        _profilePlayer.Shop.OnFailedPurchase.SubscribeOnChange(OnFailedPurchase);
    }


    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObjects(objectView);
        
        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
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

