using Profile;
using UnityEngine;
using System.Collections.Generic;
using Shop;

public class Root : MonoBehaviour
{
    [SerializeField] 
    private Transform _placeForUi;
    [SerializeField]
    private List<ShopProduct> _products = new List<ShopProduct>()
    {
        new ShopProduct()
        {
            CurrentProductType = UnityEngine.Purchasing.ProductType.Consumable,
            Id = "com.mycompany.cargame.SmallCoins"
        }
    };
    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f, new ShopTools(_products));
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
