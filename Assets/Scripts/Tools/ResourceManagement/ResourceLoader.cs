using UnityEngine;

public static class ResourceLoader
{
    public static GameObject LoadPrefab(ResourcePath path)
    {
        return Resources.Load<GameObject>(path.PathResource);
    }
    public static T LoadObject<T>(ResourcePath path) where T : Object
    {
        return Resources.Load<T>(path.PathResource);
    }
    public static T LoadAndInstantiate<T>(ResourcePath path, Transform parent) where T : MonoBehaviour
    {
        var c = LoadPrefab(path).GetComponent<T>();
        return GameObject.Instantiate<T>(c, parent);
    }
    public static T LoadView<T>(ResourcePath path) where T : MonoBehaviour
    {
        return LoadPrefab(path).GetComponent<T>();
    }
} 
