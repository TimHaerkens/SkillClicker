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
        public int clicks; //How many clicks to get loot FILL THIS WITH FUNCTION
        //public int xp; //How many xp do you get for mining this ore
        public string skill; //What skill am I training this for
        public string requiredTool; //What sort of tool do you need to mine this ore
        public int requiredToolLevel; //What is the required minimum level of the tool to mine this

        [System.Serializable]
        public struct ingredientData { public InventoryItemBase ingredient; public uint amount; }

        public ingredientData[] ingredients; //What do I need to make this

        public Sprite icon; //What is the image of the icon
        public Sprite image; //What is the image of the item in the gathering screen

        public string clickSound; //What is the source of the sound that you hear when you click

        public InventoryItemBase[] loot; //What are the items you can get from this

        public Gatherable(string n, int l, int c, int x, string sk, string rt, int rtl, ingredientData[] ingr, Sprite ic, Sprite img, string cs, InventoryItemBase[] lt)
        {
            name = n;
            level = l;
            skill = sk;
            requiredTool = rt;
            requiredToolLevel = rtl;

            ingredients = ingr;

            icon = ic;
            image = img;

            clickSound = cs;

            loot = lt;
        }

        public int Clicks()
        {
            float baseClicks = 20;
            //Debug.Log("You must click " + (baseClicks + (level * Mathf.Log(level, 1.21f))) + " times for " + name);
            return Mathf.RoundToInt(baseClicks + (level * Mathf.Log(level, 1.21f)));
        }

        public float xp()
        {
            float baseXP = 10;
            Debug.Log("You get " + (baseXP + (level*Mathf.Log(level, 1.9f))) + " xp");
            return baseXP + (level * Mathf.Log(level, 1.9f));
        }
    }

    [System.Serializable]
    public class Collection
    {
        public string name;//What is this collection called
        public string skill; //What skill am I training this for

        public Sprite icon; //What is the image of the icon

        public Gatherable[] gatherables; //What are the items you can get in this collection

        public Collection(string n, string s, Sprite i, Gatherable[] gat)
        {
            name = n;
            skill = s;
            icon = i;
            gatherables = gat;
        }
    }

    public Gatherable[] Ores;
    public Gatherable[] Trees;
    public Gatherable[] Runes;
    public Gatherable[] Potions;
    public Gatherable[] Gems;
    public Gatherable[] Scrolls;
    public Collection[] FishingSpots;
    public Collection[] GatherSpots;
    public Gatherable[] Recipes;
    public Gatherable[] Blueprints;



    public Button gatherableButton;
    public Button collectionButton;


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
        SpawnMagics();
        SpawnScrolls();




    }

    //Spawn resources in the current area
    public void SpawnResources()
    {
        foreach (Button b in resourcebuttons) Destroy(b.gameObject);
        resourcebuttons.Clear();
        foreach (Area.Resource r in player.currentArea.GetComponent<Area>().resources)
        {
            if(r.type=="FishingSpot" || r.type == "GatherSpot")Spawn(r.type, r.id, "collection");
            else Spawn(r.type, r.id, "resource");
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

    //Spawn magic things in the current area
    public void SpawnMagics()
    {
        foreach (Button b in magicbuttons) Destroy(b.gameObject);
        magicbuttons.Clear();
        foreach (Area.Resource r in player.currentArea.GetComponent<Area>().magics)
        {
            Spawn(r.type, r.id, "magic");
        }
    }

    //Spawn potions in the current area
    public void SpawnScrolls()
    {
        foreach (Button b in scrollbuttons) Destroy(b.gameObject);
        scrollbuttons.Clear();
        foreach (Area.Resource r in player.currentArea.GetComponent<Area>().scrolls)
        {
            Spawn(r.type, r.id, "scroll");
        }
    }

    public List<Button> collectionbuttons = new List<Button>();
    public List<Button> resourcebuttons = new List<Button>();
    public List<Button> recipebuttons = new List<Button>();
    public List<Button> blueprintbuttons = new List<Button>();
    public List<Button> magicbuttons = new List<Button>();
    public List<Button> scrollbuttons = new List<Button>();

    public Button fakeButton;

    //Spawn a button
    void Spawn(string category, int id, string type)
    {
        var thisInfo = new Gatherable("", 0, 0, 0, "", "", 0, new Gathering.Gatherable.ingredientData[0], new Sprite(), new Sprite(), "", new InventoryItemBase[0]);
        var thisInfo2 = new Collection("","",new Sprite(),new Gatherable[0]);
        //GameObject newGatherable = Instantiate(gatherableIcon, gatherableIcon.transform.position, Quaternion.identity) as GameObject;
        Button newButton = fakeButton;
        if(type == "collection")
        {
            newButton = Instantiate(collectionButton, collectionButton.transform.position, Quaternion.identity) as Button;
            resourcebuttons.Add(newButton);
            thisInfo2 = newButton.GetComponent<CollectionScript>().myInfo;
            if(category=="FishingSpot")newButton.transform.parent = GameObject.Find("Training Tab").transform.FindChild("FishingSpots").transform;
            if(category=="GatherSpot")newButton.transform.parent = GameObject.Find("Training Tab").transform.FindChild("GatherSpots").transform;
        }
        if (type == "resource")
        {
            newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
            resourcebuttons.Add(newButton);
            thisInfo = newButton.GetComponent<GatherableScript>().myInfo;
            //Setting the parent
            if(category=="Ore")newButton.transform.parent = GameObject.Find("Training Tab").transform.FindChild("Ores").transform;
            if(category=="Tree")newButton.transform.parent = GameObject.Find("Training Tab").transform.FindChild("Trees").transform;

        }
        if (type == "recipe")
        {
            newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
            newButton.transform.parent = GameObject.Find("Kitchen Tab").transform.FindChild("Recipes").transform;
            recipebuttons.Add(newButton);
            thisInfo = newButton.GetComponent<GatherableScript>().myInfo;
        }
        if (type == "blueprint")
        {
            newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
            newButton.transform.parent = GameObject.Find("Workshop Tab").transform.FindChild("Blueprints").transform;
            blueprintbuttons.Add(newButton);
            thisInfo = newButton.GetComponent<GatherableScript>().myInfo;
        }
        if (type == "magic")
        {
            newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
            magicbuttons.Add(newButton);
            thisInfo = newButton.GetComponent<GatherableScript>().myInfo;
            //Setting the parent
            if (category == "Potion") newButton.transform.parent = GameObject.Find("MageTower Tab").transform.FindChild("Potions").transform;
            if (category == "Gem") newButton.transform.parent = GameObject.Find("MageTower Tab").transform.FindChild("Gems").transform;
        }
        if (type == "scroll")
        {
            newButton = Instantiate(gatherableButton, gatherableButton.transform.position, Quaternion.identity) as Button;
            scrollbuttons.Add(newButton);
            thisInfo = newButton.GetComponent<GatherableScript>().myInfo;
            if (category == "Scroll") newButton.transform.parent = GameObject.Find("Library Tab").transform.FindChild("Scrolls").transform;
        }


        switch (category)
        {
            case "Ore":
                thisInfo.name =                 Ores[id].name;
                thisInfo.level =                Ores[id].level;
                thisInfo.clicks =               Ores[id].Clicks();
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
                thisInfo.clicks =               Trees[id].Clicks();
                thisInfo.skill =                Trees[id].skill;
                thisInfo.requiredTool =         Trees[id].requiredTool;
                thisInfo.icon =                 Trees[id].icon;
                thisInfo.image =                Trees[id].image;
                thisInfo.clickSound =           Trees[id].clickSound;
                thisInfo.loot =                 Trees[id].loot;
                break;
            case "FishingSpot":
                thisInfo2.name =                FishingSpots[id].name;
                thisInfo2.skill =               FishingSpots[id].skill;
                thisInfo2.icon =                FishingSpots[id].icon;
                thisInfo2.gatherables =         FishingSpots[id].gatherables;
                foreach (Gatherable g in thisInfo2.gatherables) g.clicks = g.Clicks();
                break;
            case "GatherSpot":
                thisInfo2.name =                GatherSpots[id].name;
                thisInfo2.skill =               GatherSpots[id].skill;
                thisInfo2.icon =                GatherSpots[id].icon;
                thisInfo2.gatherables =         GatherSpots[id].gatherables;
                foreach (Gatherable g in thisInfo2.gatherables) g.clicks = g.Clicks();
                break;
            case "Recipe":
                thisInfo.name =                 Recipes[id].name;
                thisInfo.level =                Recipes[id].level;
                thisInfo.clicks =               Recipes[id].Clicks();
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
                thisInfo.clicks =               Blueprints[id].Clicks();
                thisInfo.skill =                Blueprints[id].skill;
                thisInfo.requiredTool =         Blueprints[id].requiredTool;
                thisInfo.ingredients =          Blueprints[id].ingredients;
                thisInfo.icon =                 Blueprints[id].icon;
                thisInfo.image =                Blueprints[id].image;
                thisInfo.clickSound =           Blueprints[id].clickSound;
                thisInfo.loot =                 Blueprints[id].loot;
                break;
            case "Potion":
                thisInfo.name = Potions[id].name;
                thisInfo.level = Potions[id].level;
                thisInfo.clicks = Potions[id].Clicks();
                thisInfo.skill = Potions[id].skill;
                thisInfo.requiredTool = Potions[id].requiredTool;
                thisInfo.ingredients = Potions[id].ingredients;
                thisInfo.icon = Potions[id].icon;
                thisInfo.image = Potions[id].image;
                thisInfo.clickSound = Potions[id].clickSound;
                thisInfo.loot = Potions[id].loot;
                break;
            case "Gem":
                thisInfo.name = Gems[id].name;
                thisInfo.level = Gems[id].level;
                thisInfo.clicks = Gems[id].Clicks();
                thisInfo.skill = Gems[id].skill;
                thisInfo.requiredTool = Gems[id].requiredTool;
                thisInfo.ingredients = Gems[id].ingredients;
                thisInfo.icon = Gems[id].icon;
                thisInfo.image = Gems[id].image;
                thisInfo.clickSound = Gems[id].clickSound;
                thisInfo.loot = Gems[id].loot;
                break;
            case "Scroll":
                thisInfo.name = Scrolls[id].name;
                thisInfo.level = Scrolls[id].level;
                thisInfo.clicks = Scrolls[id].Clicks();
                thisInfo.skill = Scrolls[id].skill;
                thisInfo.requiredTool = Scrolls[id].requiredTool;
                thisInfo.ingredients = Scrolls[id].ingredients;
                thisInfo.icon = Scrolls[id].icon;
                thisInfo.image = Scrolls[id].image;
                thisInfo.clickSound = Scrolls[id].clickSound;
                thisInfo.loot = Scrolls[id].loot;
                break;
        }

        if(type=="collection")newButton.GetComponent<CollectionScript>().UpdateIcon();
        else newButton.GetComponent<GatherableScript>().UpdateIcon();
    }




}
