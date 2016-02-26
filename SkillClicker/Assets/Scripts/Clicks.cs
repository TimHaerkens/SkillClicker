using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;

public class Clicks : MonoBehaviour {

    public Text aantalClicks;
    public int clicks = 0;

    public Gathering.Gatherable info;

    


   
	
	
	void Update () {
        aantalClicks.text = "";
        for(int i = 0; i < info.clicks - clicks; i++)
        {
            aantalClicks.text += ".";
        }
	}

    public void Clicked()
    {

        FMODUnity.RuntimeManager.PlayOneShot("event:/"+info.clickSound, transform.position);
        clicks += 1;
        if (clicks >= info.clicks)
        {
            Loot();
            clicks = 0;
        }
    }

    void Loot()
    {
        Debug.Log(info.loot[0]);
        InventoryManager.AddItem(info.loot[0]);

    }
}
