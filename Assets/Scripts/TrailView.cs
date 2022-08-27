using JoostenProductions;
using Tools;
using UnityEngine;

public class TrailView : MonoBehaviour
{
    public void Init()
    {
        UpdateManager.SubscribeToUpdate(Follow);
    }
    private void OnDestroy()
    {
        UpdateManager.UnsubscribeFromUpdate(Follow);
    }
    private void Follow()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }
}
