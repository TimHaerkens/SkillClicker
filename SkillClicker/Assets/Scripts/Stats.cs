using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Stats : MonoBehaviour {

    private static Stats Instance;
    public static Stats instance
    {
        get
        {
            if (Instance == null)
                Instance = GameObject.FindObjectOfType<Stats>();
            return Instance;
        }
    }

    public GameObject statPrefab;

    public GameObject content;


    public Text[] texts;
    Dictionary<string, Stat> statItems = new Dictionary<string, Stat>();
    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
        {
            if (this != Instance)
                Destroy(this.gameObject);
        }



        AddStat("Times Clicked", "stat_timesclicked");
        AddStat("Seconds Wasted", "stat_secondswasted");

        AddStat("Money Earned", "stat_moneyearned");
        AddStat("Money Spent", "stat_moneyspent");

        AddStat("Monsters Killed", "stat_monsterskilled");
        AddStat("Times Died", "stat_timesdied");
        AddStat("Treasures Found", "stat_treasuresfound");

        AddStat("Ores Mined", "stat_oresmined");
        AddStat("Logs Chopped", "stat_logschopped");
        AddStat("Fish Caught", "stat_fishcaught");
        AddStat("Materials Gathered", "stat_materialsgathered");
        AddStat("Meals Cooked", "stat_mealscooked");
        AddStat("Items Crafted", "stat_itemscrafted");
        AddStat("Items Studied", "stat_itemsstudied");






    }

    void AddStat(string name, string pref)
    {
        GameObject newStat = Instantiate(statPrefab, transform.position, Quaternion.identity) as GameObject;
        newStat.transform.parent = content.transform;
        newStat.transform.localScale = new Vector2(1, 1);

        Stat stat = newStat.GetComponent<Stat>();

        stat.name = name;
        stat.pref = pref;
        stat.UpdateValue();

        statItems.Add(pref, stat);

    }


    void Update()
    {
        UpdateStat("stat_secondswasted", PlayerPrefs.GetFloat("stat_secondswasted") + Time.deltaTime);
        

    }


    public void UpdateStat(string stat, float value)
    {
        PlayerPrefs.SetFloat(stat, value);
        statItems[stat].UpdateValue();
    }
}
