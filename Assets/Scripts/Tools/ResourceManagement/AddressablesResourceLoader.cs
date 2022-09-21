using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressablesResourceLoader 
{
    public static RectTransform _mountRootTransform;

    public static AssetReferenceGameObject _loadPrefab;

    public static List<AsyncOperationHandle<GameObject>> _addressablePrefabs = new List<AsyncOperationHandle<GameObject>>();

    public static IDictionary<string, AssetReferenceGameObject> _prefabs = new Dictionary<string, AssetReferenceGameObject>();

    public static void PopulatePrefabs(List<AddressablePrefabCfg> cfgs)
    {
        foreach (var cfg in cfgs)
        {
            _prefabs.Add(new KeyValuePair<string, AssetReferenceGameObject>(cfg.Title, cfg.GameObject));
        }
    }
    public static void OnDestroy()
    {
        foreach (var addressablePrefab in _addressablePrefabs)
            Addressables.ReleaseInstance(addressablePrefab);

        _addressablePrefabs.Clear();
    }
    public static GameObject CreatePrefab(string key, Transform parent)
    {
        AssetReferenceGameObject reference;
        if (_prefabs.TryGetValue(key, out reference))
        {
            var addressablePrefab = Addressables.InstantiateAsync(reference, _mountRootTransform);
            _addressablePrefabs.Add(addressablePrefab);
            return addressablePrefab.Task.Result;
        }
        else
            return null;
    }

    public static IEnumerator UnloadAssets()
    {
        yield return new WaitForSeconds(4f);
        foreach (var go in _addressablePrefabs)
        {
            Addressables.ReleaseInstance(go);
        }
    }
}
