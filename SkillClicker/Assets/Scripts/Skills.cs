using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skills : MonoBehaviour {

    static SkillsInfo skillsInfo;

    static List<float> XPTable = new List<float>();

    [System.Serializable]
	public class Skill
    {
        public string name;
        public int level;
        public float xp;

        public void LevelUp()
        {
            //FMODUnity.RuntimeManager.PlayOneShot("event:/LevelUp");
            GameManager.instance.Celebration();
            level++;
            GameManager.instance.ShowNotification(name + " Level " + level + " achieved!");

        }

        public void Reset()
        {
            PlayerPrefs.SetFloat("xp_" + name, 0);
            xp = 0;
            GameObject.Find("Player").GetComponent<Skills>().UpdateSkillsInfo(name, 0);
        }

        public void GetXP(float amount)
        {
            xp += amount;
            if (xp >= XPTable[level+1]) LevelUp();

            //Update playerprefs
            PlayerPrefs.SetFloat("xp_" + name, xp);
        }


        public float LevelProgress()
        {
            int currentlvl = CalculateLevel(xp);
            float requiredXPThisLevel = XPTable[currentlvl + 1] - XPTable[currentlvl];
            float percentage = (xp-XPTable[currentlvl]) / requiredXPThisLevel * 100;
            
            return percentage;
        }

    }

    
    private float ExpAtLevel(int baseExp, float curve, int level)
    {
        float current = baseExp * (Mathf.Pow(curve, level) - Mathf.Pow(1, level)) * 10;
        return current;
    }

    void Awake()
    {
        for(int level = 0; level < 50; level++)
        {
            float total = ExpAtLevel(50,1.1f, level-1);
            XPTable.Add(total);
        }

        skillsInfo = GameObject.Find("Skills Tab").GetComponent<SkillsInfo>();
        foreach(Skill s in skills)
        {
            s.xp = PlayerPrefs.GetFloat("xp_" + s.name);
            UpdateSkillsInfo(s.name, s.xp);

        }
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

        skillsInfo.levels[id].text = CalculateLevel(xp).ToString();
        skills[id].level = CalculateLevel(skills[id].xp);
    }

    static int CalculateLevel(float xp)
    {
        int tempLevel = 0;
        for (int i = 0; i < XPTable.Count; i++)
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
