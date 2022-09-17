using Tools;
using System.Collections.Generic;
using UnityEngine;
using Profile;

public class GameController : BaseController
{
    private readonly ProfilePlayer _profilePlayer;
    private readonly Transform _placeForUi;
    private readonly GameView _gameView;
    public GameController(ProfilePlayer profilePlayer, List<AbilityItemCfg> abilities, Transform placeForUi)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _gameView = ResourceLoader.LoadAndInstantiate<GameView>(new ResourcePath() { PathResource = "Prefabs/GameView" }, _placeForUi);
        _gameView.Init(StartFight);
        AddGameObjects(_gameView.gameObject);

        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();
        
        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);
            
        var carController = new CarController();
        AddController(carController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar, carController.GetViewObject());
        AddController(inputGameController);

        var repository = new AbilitiesRepository(abilities);
        AddController(repository);

        var abilitiesController = new AbilitiesController(carController, repository, profilePlayer, placeForUi);
        AddController(abilitiesController);
    }
    private void StartFight()
    {
        _profilePlayer.CurrentState.Value = GameState.Fight;
    }
}

