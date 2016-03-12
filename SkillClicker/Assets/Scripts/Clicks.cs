using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Devdog.InventorySystem.Models;

public class Clicks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Text aantalClicks;
    public int clicks = 0;

    public Gathering.Gatherable info;//Info of the clickable
    public Gathering.Collection info2;//Info of the collection
    public Skills skills;

    public GatheringScreen gatheringScreen;
    public Stats stats;

    public Text amount;

    public CharacterUI characterUI;

    void Awake()
    {
        characterUI = GameObject.Find("Character Tab").GetComponent<CharacterUI>();

    }


    void Update () {


        UpdatePuntjes();

        if (_pressed)
        {
            if(info.skill=="Cooking"|| info.skill == "Fishing") Hold();
        }
        else
        {
            if (info.skill == "Cooking")
            {
                CoolOff();
            }
        }

        

    }

    void UpdatePuntjes()
    {
        aantalClicks.text = "";
        for (int i = 0; i < info.clicks - clicks; i++)
        {
            aantalClicks.text += ".";
        }
    }


    public float timeMouseDown;
    public float timeNotHolding;
    public Image progressRing_pfb;
    Image progressRing;

    bool _pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
        if (info.skill == "Cooking")
        {
            Image ring = Instantiate(progressRing_pfb, eventData.pressPosition, Quaternion.identity) as Image;
            ring.transform.parent = transform.parent.transform;
            ring.transform.localScale = new Vector2(1, 1);
            ring.name = "ProgressRing";
            progressRing = ring;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        timeMouseDown = 0;
        _pressed = false;
        Destroy(GameObject.Find("ProgressRing"));
        Clicked();
    }

   


    void CoolOff()
    {
        timeNotHolding += Time.deltaTime;
        if (timeNotHolding > 0.5f)
        {
            if (clicks > 0) clicks--;
            timeNotHolding = 0;
        }
    }


    public ItemCollectionBase inventory;
    public void Clicked()
    {
        if(inventory.GetEmptySlotsCount()==0)
        {
            GameManager.instance.ShowNotification("Inventory is full");
            return;
        }

        if(info.skill == "Cooking" || info.skill == "Crafting")
        {
            if (Remaining() == 0)
            {
                GameManager.instance.ShowNotification("You don't have the required ingredients");
                return;
            }
        }

        //FMODUnity.RuntimeManager.PlayOneShot("event:/"+info.clickSound, transform.position);
        Stats.instance.UpdateStat("stat_timesclicked", PlayerPrefs.GetFloat("stat_timesclicked") + 1);
        clicks += ToolPower();
        if (clicks >= info.clicks && !_pressed)
        {
            UpdateStats();
            TakeIngredients();
            Loot(WhatLoot());
            GetXP();
            clicks = 0;
            UpdateAmount();
            if(info.skill=="Fishing" || info.skill=="Gathering")GetNewFromCollection();//Fishing is a random collection
        }
    }

    public void UpdateStats()
    {
        switch(info.skill)
        {
            case "Mining":
                Stats.instance.UpdateStat("stat_oresmined", PlayerPrefs.GetFloat("stat_oresmined") + 1);
                break;
            case "Woodcutting":
                Stats.instance.UpdateStat("stat_logschopped", PlayerPrefs.GetFloat("stat_logschopped") + 1);
                break;
            case "Fishing":
                Stats.instance.UpdateStat("stat_fishcaught", PlayerPrefs.GetFloat("stat_fishcaught") + 1);
                break;
            case "Gathering":
                Stats.instance.UpdateStat("stat_materialsgathered", PlayerPrefs.GetFloat("stat_materialsgathered") + 1);
                break;
            case "Cooking":
                Stats.instance.UpdateStat("stat_mealscooked", PlayerPrefs.GetFloat("stat_mealscooked") + 1);
                break;
            case "Crafting":
                Stats.instance.UpdateStat("stat_itemscrafted", PlayerPrefs.GetFloat("stat_itemscrafted") + 1);
                break;
            case "Knowledge":
                Stats.instance.UpdateStat("stat_itemsstudied", PlayerPrefs.GetFloat("stat_itemsstudied") + 1);
                break;
        }

    }

    public void Hold()
    {
        if (progressRing != null)
        {
            progressRing.rectTransform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            progressRing.color = Color.Lerp(Color.white, Color.green, ((float)clicks / (float)info.clicks));
            if (clicks >= info.clicks + 6) progressRing.color = Color.Lerp(Color.green, Color.red, ((float)clicks / (info.clicks + 8.0f)));
        }

        timeMouseDown += Time.deltaTime;
        if(timeMouseDown>0.2f)
        {
            Clicked();
            timeMouseDown = 0;
        }
    }

    public void UpdateAmount()
    {
        Remaining();
    }

    InventoryItemBase WhatLoot()
    {
        InventoryItemBase whatLoot = new InventoryItemBase();

        if (info.skill == "Cooking")
        {
            if (clicks > info.clicks + 8) return info.loot[1];
            else return info.loot[0];

            //If classic cooking instead of holding
            int cookingLevel = GameObject.Find("Player").GetComponent<Skills>().skills[7].level;
            float succesChance = 60 + ((cookingLevel - info.level) * 20);
            Debug.Log("chance: " + succesChance);
            if (Random.Range(1, 101) <= succesChance) whatLoot = info.loot[0];//Cooked version
            else whatLoot = info.loot[1];//Burnt version
        }

        else
        {
            whatLoot = info.loot[Random.Range(0,info.loot.Length)]; //Just the first item
        }

        return whatLoot;
    }


    //Check how much you can gather left
    int Remaining()
    {
        if (info.ingredients.Length == 0)
        {
            //amount.text = "";
            return 100;//No ingredients required
        }

        int remaining = 0;
        //Calculate how much you can make if this needs ingredients
        foreach (Gathering.Gatherable.ingredientData id in info.ingredients)
        {
            var allOfID = InventoryManager.FindAll(id.ingredient.ID, false);
            int total = 0;
            foreach (InventoryItemBase item in allOfID)
            {
                total += Mathf.FloorToInt(item.currentStackSize/id.amount);
            }
            if (total == 0)
            {
                //amount.text = "0";
                return 0;//One of the ingredients lacking
            }
            if(total < remaining || remaining == 0)
            {
                //Else update ingredients based on items that you have fewest or not yet set
                remaining = total;
                Debug.Log(remaining);
            }
        }
        //amount.text = remaining.ToString();
        return remaining;
    }

    void TakeIngredients()
    {
        if(info.ingredients.Length>0)
        {
            foreach (Gathering.Gatherable.ingredientData id in info.ingredients)
            {
                InventoryManager.RemoveItem(id.ingredient.ID, id.amount, false);
            }
        }
    }

    int ToolPower()
    {
        float tempToolpower = 1;

        //Calculate tool power
        float tl = GetToolLevel();
        float sl = GetSkillLevel();

        tempToolpower = (tl * (Mathf.Pow(1.01f,tl))) + ((sl * 0.2f));

        Debug.Log(tl + " + " + sl * 0.2f + " = " + Mathf.FloorToInt(tempToolpower));

        if (GameManager.instance.development) return Mathf.FloorToInt(tempToolpower * 2);

        return Mathf.FloorToInt(tempToolpower);
    }

    float GetToolLevel()
    {
        string skill = info.skill;
        if (skill == "Cooking" || skill == "Crafting" || skill == "Knowledge" || skill == "Magic") return 1;
        InventoryItemBase item = new InventoryItemBase();
        float tl = 0;

        var allCharacterItems = characterUI.items;

        bool toolFound = false;
        foreach (InventoryUIItemWrapperBase wrapper in allCharacterItems)
        {
            if (wrapper.item == null) continue;
            foreach (InventoryItemPropertyLookup pr in wrapper.item.properties)
            {
                if (pr.property.name == "Skill")
                {
                    //Debug.Log("Skill: " + pr.value);
                    if (pr.value != skill)
                    {
                       // Debug.Log("Not the good tool");
                        toolFound = false;
                    }
                    else
                    {
                        toolFound = true;
                        //Debug.Log("Tool found");
                        break;
                    }
                }
            }
            if (toolFound)
            {
                foreach (InventoryItemPropertyLookup pr in wrapper.item.properties)
                {
                    if (pr.property.name == "Level")
                    {
                        //Debug.Log("Level: " + float.Parse(pr.value));
                        return float.Parse(pr.value);
                    }
                }
            }
            else continue;
        }

        if (tl == 0) GameManager.instance.ShowNotification("You need a " + info.skill + " tool");
       
        return tl;
    }

    float GetSkillLevel()
    {
        string skill = info.skill;
        foreach(Skills.Skill s in GameObject.Find("Player").GetComponent<Skills>().skills)
        {
            if(s.name == skill)
            {
                return s.level;
            }
        }
        return 1;
    }


    void GetXP()
    {
        string skill = info.skill;
        float progress = 0;

        switch (skill)
        {
            case "Mining":
                skills.skills[1].GetXP(info.xp());
                progress =  skills.skills[1].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[1].xp);
                break;
            case "Woodcutting":
                skills.skills[2].GetXP(info.xp());
                progress =  skills.skills[2].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[2].xp);
                break;
            case "Fishing":
                skills.skills[3].GetXP(info.xp());
                progress =  skills.skills[3].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[3].xp);
                break;
            case "Gathering":
                skills.skills[4].GetXP(info.xp());
                progress =  skills.skills[4].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[4].xp);
                break;
            case "Crafting":
                skills.skills[5].GetXP(info.xp());
                progress = skills.skills[5].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[5].xp);
                break;
            case "Cooking":
                skills.skills[6].GetXP(info.xp());
                progress = skills.skills[6].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[6].xp);
                break;
            case "Knowledge":
                skills.skills[7].GetXP(info.xp());
                progress = skills.skills[7].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[7].xp);
                break;
            case "Magic":
                skills.skills[8].GetXP(info.xp());
                progress = skills.skills[8].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[8].xp);
                break;
        }

        gatheringScreen.UpdateXPBar(progress);
        


    }

    void GetNewFromCollection()
    {
        //Choose random of this collection
        List<Gathering.Gatherable> possibleSpawns = new List<Gathering.Gatherable>();
        foreach (Gathering.Gatherable g in info2.gatherables)
        {
            float level = 0;
            foreach (Skills.Skill s in skills.skills)
            {
                Debug.Log("s.name: " + s.name + "& g.name: " + g.skill);
                if (s.name == g.skill)
                {
                    Debug.Log("gevonden level: " + s.level);
                    level = s.level;
                }
            }
            if (g.level <= level)
            {
                possibleSpawns.Add(g);
                Debug.Log(g.name + " is viable");
            }
        }

        Debug.Log("Count: " + possibleSpawns.Count);
        Gathering.Gatherable spawnInfo = possibleSpawns[Random.Range(0, possibleSpawns.Count)];
        Debug.Log("choice: " + spawnInfo.name);

        gatheringScreen.info = spawnInfo;
        gatheringScreen.SetClickable();
        gatheringScreen.clickable.GetComponent<Clicks>().UpdateAmount();
    }

    public GameObject loot;//Prefab of drop
    void Loot(InventoryItemBase whatLoot)
    {
        GameObject lootSpawn = Instantiate(loot, transform.position, Quaternion.identity) as GameObject;
        lootSpawn.transform.parent = gatheringScreen.gameObject.transform;
        lootSpawn.transform.SetAsLastSibling();
        lootSpawn.transform.localScale = new Vector3(1, 1, 1);
        lootSpawn.GetComponent<Loot>().loot = whatLoot;
        lootSpawn.GetComponent<Loot>().image.sprite = whatLoot.icon;

        //FMODUnity.RuntimeManager.PlayOneShot("event:/succes", transform.position);


    }
}
