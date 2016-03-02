using UnityEngine;
using System.Collections;
using Devdog.InventorySystem.UI;
using Devdog.InventorySystem;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour {

    //References
    public VendorUI vendor;
    public Text vendorName;
    public Image vendorSprite;

    public void CloseShopScreen()
    {
        vendor.GetComponent<VendorTriggerer>().CloseShop();
    }

}
