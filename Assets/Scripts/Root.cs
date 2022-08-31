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
    [SerializeField] private List<ItemCfg> _items;
    [SerializeField] private List<UpgradeItemCfg> _upgrades;
    [SerializeField] private List<AbilityItemCfg> _abilities;

    private MainController _mainController;

    private void Awake()
    {
        var profilePlayer = new ProfilePlayer(15f, new ShopTools(_products));
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, profilePlayer, _items, _upgrades, _abilities);
    }

    protected void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
