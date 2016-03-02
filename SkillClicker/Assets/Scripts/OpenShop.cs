using UnityEngine;
using System.Collections;
using Devdog.InventorySystem.UI;
using Devdog.InventorySystem;

public class OpenShop : MonoBehaviour {

    public UIWindowPage ShopTab;
    public ShopScreen shopScreen;
    public NPC npc;
    void Awake()
    {
        ShopTab = GameObject.Find("Shop Tab").GetComponent<UIWindowPage>();
        shopScreen = ShopTab.GetComponent<ShopScreen>();
    }
	public void OpenShopScreen()
    {
        shopScreen.vendorName.text = npc.name + " Shop";
        shopScreen.vendorSprite.sprite = npc.sprite;
        ShopTab.Show();

        shopScreen.vendor.GetComponent<VendorTriggerer>().OpenShop();
    }

    
}
