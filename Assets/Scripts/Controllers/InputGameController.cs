using Tools;
using UnityEngine;

public class InputGameController : BaseController
{
    public InputGameController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car)
    {
        _view = ResourceLoader.LoadAndInstantiate<BaseInputView>(_viewPath, null);
        _view.Init(leftMove, rightMove, car.Speed);
    }

    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/StickControl"};
    private BaseInputView _view;
}

