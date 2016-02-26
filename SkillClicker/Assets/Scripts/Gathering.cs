using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;

public class Gathering : MonoBehaviour
{

    [System.Serializable]
    public class Gatherable
    {
        public string name;//What is this ore called
        public int level; //What level is this ore
        public int clicks; //How many clicks to get loot
        public int xp; //How many xp do you get for mining this ore
        public string skill; //What skill am I training this for
        public string requiredTool; //What sort of tool do you need to mine this ore
        public int requiredToolLevel; //What is the required minimum level of the tool to mine this

        public Sprite icon; //What is the image of the icon
        public Sprite image; //What is the image of the item in the gathering screen

        public InventoryItemBase[] loot; //What are the items you can get from this

        public Gatherable(string n, int l, int c, int x, string sk, string rt, int rtl, Sprite ic, Sprite img)
        {
            name = n;
            level = l;
            clicks = c;
            xp = x;
            skill = sk;
            requiredTool = rt;
            requiredToolLevel = rtl;

            icon = ic;
            image = img;
        }


    }

    public Gatherable[] Ores;
    public Gatherable[] Trees;
    public Gatherable[] FishingSpots;
    public Gatherable[] GatherSpots;



    public GameObject gatherableIcon;
    public Button gatherableButton;

    //Reference to city information
    public Areas areas;

    private static Gathering Instance;

    public static Gathering instance
    {
        get
        {
            if (Instance == null)
                Instance = GameObject.FindObjectOfType<Gathering>();
            return Instance;
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            if (this != Instance)
                Destroy(this.gameObject);
        }

        foreach (Areas.Resource r in areas.currentArea.resources)
        {
            Spawn(r.type, r.id);
            print("spawn shit");
        }
       
            
        

    }

    //Spawn a button
    void Spawn(string category, int id)
    {
        //GameObject newGatherable = Instantiate(gatherableIcon, gatherableIcon.transform.position, Quaternion.identity) as GameObject;
        Button newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
        newButton.transform.parent = this.transform;
       
        Gatherable thisInfo = newButton.GetComponent<GatherableScript>().myInfo;

        switch (category)
        {
            case "Ore":
                thisInfo.name =                 Ores[id].name;
                thisInfo.level =                Ores[id].level;
                thisInfo.clicks =               Ores[id].clicks;
                thisInfo.xp =                   Ores[id].xp;
                thisInfo.skill =                Ores[id].skill;
                thisInfo.requiredTool =         Ores[id].requiredTool;
                thisInfo.requiredToolLevel =    Ores[id].requiredToolLevel;
                thisInfo.icon =                 Ores[id].icon;
                thisInfo.image =                Ores[id].image;
                thisInfo.loot =                 Ores[id].loot;
                break;
            case "Tree":
                thisInfo.name =                 Trees[id].name;
                thisInfo.level =                Trees[id].level;
                thisInfo.clicks =               Trees[id].clicks;
                thisInfo.xp =                   Trees[id].xp;
                thisInfo.skill =                Trees[id].skill;
                thisInfo.requiredTool =         Trees[id].requiredTool;
                thisInfo.requiredToolLevel =    Trees[id].requiredToolLevel;
                thisInfo.icon =                 Trees[id].icon;
                thisInfo.image =                Trees[id].image;
                thisInfo.loot =                 Trees[id].loot;
                break;
            case "Fishing":
                thisInfo.name =                 FishingSpots[id].name;
                thisInfo.level =                FishingSpots[id].level;
                thisInfo.clicks =               FishingSpots[id].clicks;
                thisInfo.xp =                   FishingSpots[id].xp;
                thisInfo.skill =                FishingSpots[id].skill;
                thisInfo.requiredTool =         FishingSpots[id].requiredTool;
                thisInfo.requiredToolLevel =    FishingSpots[id].requiredToolLevel;
                thisInfo.icon =                 FishingSpots[id].icon;
                thisInfo.image =                FishingSpots[id].image;
                thisInfo.loot =                 FishingSpots[id].loot;
                break;
            case "GatherSpots":
                thisInfo.name =                 GatherSpots[id].name;
                thisInfo.level =                GatherSpots[id].level;
                thisInfo.clicks =               GatherSpots[id].clicks;
                thisInfo.xp =                   GatherSpots[id].xp;
                thisInfo.skill =                GatherSpots[id].skill;
                thisInfo.requiredTool =         GatherSpots[id].requiredTool;
                thisInfo.requiredToolLevel =    GatherSpots[id].requiredToolLevel;
                thisInfo.icon =                 GatherSpots[id].icon;
                thisInfo.image =                GatherSpots[id].image;
                thisInfo.loot =                 GatherSpots[id].loot;
                break;
        }

        newButton.GetComponent<GatherableScript>().UpdateIcon();
    }




}
