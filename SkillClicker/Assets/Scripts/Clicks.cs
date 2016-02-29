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

    public void Clicked()
    {

        //FMODUnity.RuntimeManager.PlayOneShot("event:/"+info.clickSound, transform.position);
        stats.UpdateStat("stat_timesclicked", PlayerPrefs.GetFloat("stat_timesclicked") + 1);
        clicks += 1;
        if (clicks >= info.clicks)
        {
            Loot();
            GetXP();
            clicks = 0;
        }
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

    

    void Loot()
    {
        Debug.Log(info.loot[0] + " added");
        InventoryManager.AddItem(info.loot[0]);
        //FMODUnity.RuntimeManager.PlayOneShot("event:/succes", transform.position);


    }
}
