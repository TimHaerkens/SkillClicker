using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem.UI;


public class GatherableScript : MonoBehaviour {

    //My info
    public Gathering.Gatherable myInfo;

    //References
    public Image icon;


	public void UpdateIcon()
    {
        icon.sprite = myInfo.icon;
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }

    

    public UIWindowPage GatherTab;

    public void OpenGatherableWindow()
    {
        GatherTab = GameObject.Find("Gathering Tab").GetComponent<UIWindowPage>();
        GatherTab.Show();
        GatherTab.GetComponent<GatheringScreen>().info = myInfo;
        GatherTab.GetComponent<GatheringScreen>().SetClickable();



    }
}
