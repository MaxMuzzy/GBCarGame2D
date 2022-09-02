using UnityEngine;

public class CarController : BaseController, IAbilityActivator
{
    private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/Car"};
    private readonly CarView _carView;

    public CarController()
    {
        _carView = ResourceLoader.LoadAndInstantiate<CarView>(_viewPath, null);
    }

    public GameObject GetViewObject()
    {
        return _carView.gameObject;
    }
}

