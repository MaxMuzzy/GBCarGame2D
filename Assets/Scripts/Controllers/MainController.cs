using Profile;
using UnityEngine;
using System.Collections.Generic;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _profilePlayer.CanBomb = false;
        _placeForUi = placeForUi;
        _abilitiesCfgs = DataSourceLoader.LoadCfgs<AbilityItemCfg>(_abilitiesPath);
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private readonly List<AbilityItemCfg> _abilitiesCfgs;
    private readonly ResourcePath _abilitiesPath = new ResourcePath { PathResource = "Configs/Sources/AbilitiesSource" };
    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_profilePlayer, _abilitiesCfgs);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }
}
