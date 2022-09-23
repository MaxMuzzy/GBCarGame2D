using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationController : BaseController
{
    private LocalizationWindow _view;
    public LocalizationController(Transform parentUi)
    {
        _view = ResourceLoader.LoadAndInstantiate<LocalizationWindow>(new ResourcePath { PathResource = "Prefabs/LocalizationWindow" }, parentUi);
        _view.Init(parentUi);
        AddGameObjects(_view.gameObject);
    }
}
