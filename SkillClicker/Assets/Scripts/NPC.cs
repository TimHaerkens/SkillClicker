using UnityEngine;
using System.Collections;
using Devdog.InventorySystem;
using UnityEngine.UI;

public class NPC : MonoBehaviour {

    public string name; // What is the name of the npc
    public Sprite sprite; // How does the npc look
    public Sprite buttonSprite; //What does the button of this npc look

    public bool hasShop;

    //References
    public Image myImage;
    public Skills skills;
    public TalkScreen talkScreen;
    public Stats stats;


    void Awake()
    {
        skills = GameObject.Find("Player").GetComponent<Skills>();
        talkScreen = GameObject.Find("Talk Tab").GetComponent<TalkScreen>();
        stats = GameObject.Find("Stats Tab").GetComponent<Stats>();
        myImage = GetComponent<Image>();

        GetComponent<Button>().onClick.AddListener(() => { Clicked();  });


    }

    public void Clicked()
    {
        GameManager.instance.ShowNotification("Ouch!");
    }

    

}
