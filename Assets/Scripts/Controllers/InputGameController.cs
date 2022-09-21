using Tools;
using UnityEngine;

public class InputGameController : BaseController
{
    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car, GameObject carObj)
    {
        //_view = ResourceLoader.LoadAndInstantiate<BaseInputView>(_viewPath, null);
        _view = AddressablesResourceLoader.CreatePrefab("StickControl", null).GetComponent<BaseInputView>();
        _view.Init(leftMove, rightMove, car.Speed);
        AddGameObjects(_view.gameObject);
        var carAnimController = new CarAnimationController(carObj, _view);
        AddController(carAnimController);
    }

    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/StickControl"};
    private BaseInputView _view;
}

