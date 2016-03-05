using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;
using System.Collections.Generic;

public class Gathering : MonoBehaviour
{

    [System.Serializable]
    public class Gatherable
    {
        public string name;//What is this gatherable called
        public int level; //What level is this gatherable
        public int clicks; //How many clicks to get loot
        public int xp; //How many xp do you get for mining this ore
        public string skill; //What skill am I training this for
        public string requiredTool; //What sort of tool do you need to mine this ore
        public int requiredToolLevel; //What is the required minimum level of the tool to mine this

        public InventoryItemBase[] ingredients; //What do I need to make this

        public Sprite icon; //What is the image of the icon
        public Sprite image; //What is the image of the item in the gathering screen

        public string clickSound; //What is the source of the sound that you hear when you click

        public InventoryItemBase[] loot; //What are the items you can get from this

        public Gatherable(string n, int l, int c, int x, string sk, string rt, int rtl, InventoryItemBase[] ingr, Sprite ic, Sprite img, string cs, InventoryItemBase[] lt)
        {
            name = n;
            level = l;
            clicks = c;
            xp = x;
            skill = sk;
            requiredTool = rt;
            requiredToolLevel = rtl;

            ingredients = ingr;

            icon = ic;
            image = img;

            clickSound = cs;

            loot = lt;
        }


    }

    public Gatherable[] Ores;
    public Gatherable[] Trees;
    public Gatherable[] FishingSpots;
    public Gatherable[] GatherSpots;
    public Gatherable[] Recipes;
    public Gatherable[] Blueprints;



    public GameObject gatherableIcon;
    public Button gatherableButton;


    public Player player;

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

        SpawnResources();
        SpawnRecipes();
        SpawnBlueprints();




    }

    //Spawn resources in the current area
    public void SpawnResources()
    {
        foreach (Button b in resourcebuttons) Destroy(b.gameObject);
        resourcebuttons.Clear();
        foreach (Area.Resource r in player.currentArea.GetComponent<Area>().resources)
        {
            Spawn(r.type, r.id, "resource");
        }
    }

    //Spawn recipes in the current area
    public void SpawnRecipes()
    {
        foreach (Button b in recipebuttons) Destroy(b.gameObject);
        recipebuttons.Clear();
        foreach (Area.Resource r in player.currentArea.GetComponent<Area>().recipes)
        {
            Spawn(r.type, r.id, "recipe");
        }
    }

    //Spawn blueprints in the current area
    public void SpawnBlueprints()
    {
        foreach (Button b in blueprintbuttons) Destroy(b.gameObject);
        blueprintbuttons.Clear();
        foreach (Area.Resource r in player.currentArea.GetComponent<Area>().blueprints)
        {
            Spawn(r.type, r.id, "blueprint");
        }
    }

    public List<Button> resourcebuttons = new List<Button>();
    public List<Button> recipebuttons = new List<Button>();
    public List<Button> blueprintbuttons = new List<Button>();

    //Spawn a button
    void Spawn(string category, int id, string type)
    {
        //GameObject newGatherable = Instantiate(gatherableIcon, gatherableIcon.transform.position, Quaternion.identity) as GameObject;
        Button newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
        if (type == "resource")
        {
            resourcebuttons.Add(newButton);
            newButton.transform.parent = GameObject.Find("Training Tab").transform.FindChild("Gatherables").transform;
        }
        if (type == "recipe")
        {
            newButton.transform.parent = GameObject.Find("Kitchen Tab").transform.FindChild("Recipes").transform;
            recipebuttons.Add(newButton);
        }
        if (type == "blueprint")
        {
            newButton.transform.parent = GameObject.Find("Workshop Tab").transform.FindChild("Blueprints").transform;
            blueprintbuttons.Add(newButton);
        }

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
                thisInfo.icon =                 Ores[id].icon;
                thisInfo.image =                Ores[id].image;
                thisInfo.clickSound =           Ores[id].clickSound;
                thisInfo.loot =                 Ores[id].loot;
                break;
            case "Tree":
                thisInfo.name =                 Trees[id].name;
                thisInfo.level =                Trees[id].level;
                thisInfo.clicks =               Trees[id].clicks;
                thisInfo.xp =                   Trees[id].xp;
                thisInfo.skill =                Trees[id].skill;
                thisInfo.requiredTool =         Trees[id].requiredTool;
                thisInfo.icon =                 Trees[id].icon;
                thisInfo.image =                Trees[id].image;
                thisInfo.clickSound =           Trees[id].clickSound;
                thisInfo.loot =                 Trees[id].loot;
                break;
            case "Fishing":
                thisInfo.name =                 FishingSpots[id].name;
                thisInfo.level =                FishingSpots[id].level;
                thisInfo.clicks =               FishingSpots[id].clicks;
                thisInfo.xp =                   FishingSpots[id].xp;
                thisInfo.skill =                FishingSpots[id].skill;
                thisInfo.requiredTool =         FishingSpots[id].requiredTool;
                thisInfo.icon =                 FishingSpots[id].icon;
                thisInfo.image =                FishingSpots[id].image;
                thisInfo.clickSound =           FishingSpots[id].clickSound;
                thisInfo.loot =                 FishingSpots[id].loot;
                break;
            case "GatherSpot":
                thisInfo.name =                 GatherSpots[id].name;
                thisInfo.level =                GatherSpots[id].level;
                thisInfo.clicks =               GatherSpots[id].clicks;
                thisInfo.xp =                   GatherSpots[id].xp;
                thisInfo.skill =                GatherSpots[id].skill;
                thisInfo.requiredTool =         GatherSpots[id].requiredTool;
                thisInfo.icon =                 GatherSpots[id].icon;
                thisInfo.image =                GatherSpots[id].image;
                thisInfo.clickSound =           GatherSpots[id].clickSound;
                thisInfo.loot =                 GatherSpots[id].loot;
                break;
            case "Recipe":
                thisInfo.name =                 Recipes[id].name;
                thisInfo.level =                Recipes[id].level;
                thisInfo.clicks =               Recipes[id].clicks;
                thisInfo.xp =                   Recipes[id].xp;
                thisInfo.skill =                Recipes[id].skill;
                thisInfo.requiredTool =         Recipes[id].requiredTool;
                thisInfo.ingredients =          Recipes[id].ingredients;
                thisInfo.icon =                 Recipes[id].icon;
                thisInfo.image =                Recipes[id].image;
                thisInfo.clickSound =           Recipes[id].clickSound;
                thisInfo.loot =                 Recipes[id].loot;
                break;
            case "Blueprint":
                thisInfo.name =                 Blueprints[id].name;
                thisInfo.level =                Blueprints[id].level;
                thisInfo.clicks =               Blueprints[id].clicks;
                thisInfo.xp =                   Blueprints[id].xp;
                thisInfo.skill =                Blueprints[id].skill;
                thisInfo.requiredTool =         Blueprints[id].requiredTool;
                thisInfo.ingredients =          Blueprints[id].ingredients;
                thisInfo.icon =                 Blueprints[id].icon;
                thisInfo.image =                Blueprints[id].image;
                thisInfo.clickSound =           Blueprints[id].clickSound;
                thisInfo.loot =                 Blueprints[id].loot;
                break;
        }

        newButton.GetComponent<GatherableScript>().UpdateIcon();
    }




}
