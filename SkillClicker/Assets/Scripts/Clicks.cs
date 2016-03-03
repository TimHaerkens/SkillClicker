using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Clicks : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Text aantalClicks;
    public int clicks = 0;

    public Gathering.Gatherable info;
    public Skills skills;

    public GatheringScreen gatheringScreen;
    public Stats stats;

    public Text amount;

    

    void Update () {
        aantalClicks.text = "";
        for(int i = 0; i < info.clicks - clicks; i++)
        {
            aantalClicks.text += ".";
        }

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


    public float timeMouseDown;
    public float timeNotHolding;

    bool _pressed = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        _pressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pressed = false;
    }




    void CoolOff()
    {
        timeNotHolding += Time.deltaTime;
        if (timeNotHolding > 1)
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

        if(info.skill == "Cooking")
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
        if (clicks >= info.clicks)
        {
            TakeIngredients();
            Loot(WhatLoot());
            GetXP();
            clicks = 0;
            UpdateAmount();
        }
    }

    public void Hold()
    {
        timeMouseDown += Time.deltaTime;
        if(timeMouseDown>0.5f)
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

        if(info.skill=="Cooking")
        {
            int cookingLevel = GameObject.Find("Player").GetComponent<Skills>().skills[7].level;
            float succesChance = 60 + ((cookingLevel-info.level)*20);
            Debug.Log("chance: " + succesChance);
            if (Random.Range(1, 101) <= succesChance)   whatLoot = info.loot[0];//Cooked version
            else                                        whatLoot = info.loot[1];//Burnt version
        }

        else whatLoot = info.loot[0]; //Just the first item

        return whatLoot;
    }

    //Check how much you can gather left
    int Remaining()
    {
        if (info.ingredients.Length == 0)
        {
            amount.text = "";
            return 100;//No ingredients required
        }

        int remaining = 0;
        //Calculate how much you can make if this needs ingredients
        foreach (InventoryItemBase ing in info.ingredients)
        {
            var allOfID = InventoryManager.FindAll(info.ingredients[0].ID, false);
            if (allOfID.Count == 0) return 0;//One of the ingredients lacking
            if(allOfID.Count < remaining || remaining == 0)
            {
                //Else update ingredients based on items that you have fewest or not yet set
                remaining = allOfID.Count;
            }
        }

        return remaining;
    }

    void TakeIngredients()
    {
        if(info.ingredients.Length>0)
        {
            foreach (InventoryItemBase ing in info.ingredients)
            {
                InventoryManager.RemoveItem(info.ingredients[0].ID, 1, false);
            }
        }
    }

    int ToolPower()
    {
        if (GameManager.instance.development) return 2;


        return 1;
    }

    void GetXP()
    {
        string skill = info.skill;
        float progress = 0;

        switch (skill)
        {
            case "Mining":
                skills.skills[1].GetXP(info.xp);
                progress =  skills.skills[1].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[1].xp);
                break;
            case "Woodcutting":
                skills.skills[2].GetXP(info.xp);
                progress =  skills.skills[2].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[2].xp);
                break;
            case "Fishing":
                skills.skills[3].GetXP(info.xp);
                progress =  skills.skills[3].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[3].xp);
                break;
            case "Gathering":
                skills.skills[4].GetXP(info.xp);
                progress =  skills.skills[4].LevelProgress();
                skills.UpdateSkillsInfo(skill, skills.skills[4].xp);
                break;
        }

        gatheringScreen.UpdateXPBar(progress);
        


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
