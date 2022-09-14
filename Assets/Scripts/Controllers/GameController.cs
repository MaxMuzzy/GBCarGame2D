using Tools;
using System.Collections.Generic;
using UnityEngine;
public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, List<AbilityItemCfg> abilities)
    {
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

        var abilitiesController = new AbilitiesController(carController, repository, profilePlayer);
        AddController(abilitiesController);
    }
}

