﻿using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {

    static SkillsInfo skillsInfo;

    static float[] XPTable = new float[9]
    {
        0,
        0,
        5,
        12,
        25,
        43,
        75,
        124,
        200,
    };

    [System.Serializable]
	public class Skill
    {
        public string name;
        public int level;
        public float xp;

        public void LevelUp()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/LevelUp");
            level++;
            Debug.Log(name + " Level " + level + " achieved!");

        }

        public void GetXP(float amount)
        {
            xp += amount;
            if (xp > XPTable[level+1]) LevelUp();
        }

        public float LevelProgress()
        {
            int currentlvl = CalculateLevel(xp);
            float requiredXPThisLevel = XPTable[currentlvl + 1] - XPTable[currentlvl];
            float percentage = (xp-XPTable[currentlvl]) / requiredXPThisLevel * 100;

            
            return percentage;
        }

    }

    void Awake()
    {
        skillsInfo = GameObject.Find("Skills Tab").GetComponent<SkillsInfo>();
    }

    public void UpdateSkillsInfo(string skill, float xp)
    {
        int id = 0;
        switch (skill)
        {
            case "Attack": id = 0; break;
            case "Mining": id = 1; break;
            case "Woodcutting": id = 2; break;
            case "Fishing": id = 3; break;
            case "Gathering": id = 4; break;
            case "Crafting": id = 5; break;
            case "Cooking": id = 6; break;
            case "Knowledge": id = 7; break;
            case "Magic": id = 8; break;
        }
        Debug.Log("skill: "+ skill + " & id: " + id);

        skillsInfo.levels[id].text = CalculateLevel(xp).ToString();
    }

    static int CalculateLevel(float xp)
    {
        int tempLevel = 0;
        for (int i = 0; i < XPTable.Length; i++)
        {
            if(xp - XPTable[i]>=0)
            {
                tempLevel = i;
            }
            else
            {
                break;
            }
        }
        return tempLevel;
    }

   

    public Skill[] skills;

}
