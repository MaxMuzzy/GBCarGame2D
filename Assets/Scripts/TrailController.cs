using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : BaseController
{
    public TrailController()
    {
        _view = LoadView();
        _view.Init();
    }

    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Trail" };
    private TrailView _view;

    private TrailView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObjects(objView);

        return objView.GetComponent<TrailView>();
    }
}
