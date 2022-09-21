using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "AddressablePrefabCfg", menuName = "AddressablePrefabCfg")]
public class AddressablePrefabCfg : ScriptableObject, IConfig
{
    [SerializeField]
    private int _id;

    [SerializeField]
    private string _title;

    [SerializeField]
    private AssetReferenceGameObject _gameObject;

    public int Id => _id;

    public string Title => _title;

    public AssetReferenceGameObject GameObject => _gameObject;
}
