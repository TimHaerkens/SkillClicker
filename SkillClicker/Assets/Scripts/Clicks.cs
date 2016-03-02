using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;

public class Clicks : MonoBehaviour {

    public Text aantalClicks;
    public int clicks = 0;

    public Gathering.Gatherable info;
    public Skills skills;

    public GatheringScreen gatheringScreen;
    public Stats stats;




    void Update () {
        aantalClicks.text = "";
        for(int i = 0; i < info.clicks - clicks; i++)
        {
            aantalClicks.text += ".";
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

        //FMODUnity.RuntimeManager.PlayOneShot("event:/"+info.clickSound, transform.position);
        Stats.instance.UpdateStat("stat_timesclicked", PlayerPrefs.GetFloat("stat_timesclicked") + 1);
        clicks += ToolPower();
        if (clicks >= info.clicks)
        {
            Loot();
            GetXP();
            clicks = 0;
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


    public GameObject loot;
    void Loot()
    {
        Debug.Log(info.loot[0] + " added");
        GameObject lootSpawn = Instantiate(loot, transform.position, Quaternion.identity) as GameObject;
        lootSpawn.transform.parent = gatheringScreen.gameObject.transform;
        lootSpawn.transform.SetAsLastSibling();
        lootSpawn.transform.localScale = new Vector3(1, 1, 1);
        lootSpawn.GetComponent<Loot>().loot = info.loot[0];
        lootSpawn.GetComponent<Loot>().image.sprite = info.loot[0].icon;

        //FMODUnity.RuntimeManager.PlayOneShot("event:/succes", transform.position);


    }
}
