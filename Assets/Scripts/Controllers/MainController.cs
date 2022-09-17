using Profile;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _profilePlayer.CanBomb = false;
        _placeForUi = placeForUi;
        _abilitiesCfgs = DataSourceLoader.LoadCfgs<AbilityItemCfg>(new ResourcePath { PathResource = "Configs/Sources/AbilitiesSource" });
        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);

    }

    private MainMenuController _mainMenuController;
    private GameController _gameController;
    private DailyRewardController _dailyRewardController;
    private WeeklyRewardController _weeklyRewardController;
    private FightController _fightController;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;

    private readonly List<AbilityItemCfg> _abilitiesCfgs;
    


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
                ClearAll();
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                break;
            case GameState.Game:
                ClearAll();
                _gameController = new GameController(_profilePlayer, _abilitiesCfgs, _placeForUi);
                break;
            case GameState.Reward:
                ClearAll();
                var rewardView = ResourceLoader.LoadAndInstantiate<RewardView>(new ResourcePath() { PathResource = "Prefabs/RewardWindow" }, _placeForUi);
                var currency = ResourceLoader.LoadAndInstantiate<CurrencyWindow>(new ResourcePath() { PathResource = "Prefabs/CurrencyWindow" }, _placeForUi);

                _dailyRewardController = new DailyRewardController(_profilePlayer, currency, rewardView);
                _weeklyRewardController = new WeeklyRewardController(_profilePlayer, currency, rewardView);
                break;
            case GameState.Fight:
                ClearAll();
                var fightView = ResourceLoader.LoadAndInstantiate<FightWindowView>(new ResourcePath() { PathResource = "Prefabs/FightWindowView" }, _placeForUi);
                _fightController = new FightController(_profilePlayer, fightView);
                break;
            default:
                ClearAll();
                break;
        }
    }
    private void ClearAll()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _dailyRewardController?.Dispose();
        _weeklyRewardController?.Dispose();
        _fightController?.Dispose();
    }

}
