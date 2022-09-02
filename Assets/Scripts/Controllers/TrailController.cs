using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : BaseController
{
    public TrailController()
    {
        _view = ResourceLoader.LoadAndInstantiate<TrailView>(_viewPath, null);
        _view.Init();
    }

    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Trail" };
    private TrailView _view;
}
