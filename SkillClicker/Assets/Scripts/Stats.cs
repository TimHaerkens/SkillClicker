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

    public Text[] texts;
    Dictionary<string, Text> statTexts = new Dictionary<string, Text>();
    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
        {
            if (this != Instance)
                Destroy(this.gameObject);
        }

        statTexts.Add("stat_timesclicked", texts[0]);
        statTexts.Add("stat_secondswasted", texts[1]);
        statTexts.Add("stat_moneyearned", texts[2]);
        statTexts.Add("stat_moneyspent", texts[3]);
        statTexts.Add("stat_monsterskilled", texts[4]);
        statTexts.Add("stat_timesdied", texts[5]);
        statTexts.Add("stat_treasuresfound", texts[6]);
        statTexts.Add("stat_itemscrafted", texts[7]);
        statTexts.Add("stat_mealscooked", texts[8]);

        statTexts["stat_timesclicked"].text = PlayerPrefs.GetFloat("stat_timesclicked").ToString();
        statTexts["stat_secondswasted"].text = PlayerPrefs.GetFloat("stat_secondswasted").ToString();
        statTexts["stat_moneyearned"].text = PlayerPrefs.GetFloat("stat_moneyearned").ToString();
        statTexts["stat_moneyspent"].text = PlayerPrefs.GetFloat("stat_moneyspent").ToString();
        statTexts["stat_monsterskilled"].text = PlayerPrefs.GetFloat("stat_monsterskilled").ToString();
        statTexts["stat_timesdied"].text = PlayerPrefs.GetFloat("stat_timesdied").ToString();
        statTexts["stat_treasuresfound"].text = PlayerPrefs.GetFloat("stat_treasuresfound").ToString();
        statTexts["stat_itemscrafted"].text = PlayerPrefs.GetFloat("stat_itemscrafted").ToString();
        statTexts["stat_mealscooked"].text = PlayerPrefs.GetFloat("stat_mealscooked").ToString();


    }

    void Update()
    {
        UpdateStat("stat_secondswasted", PlayerPrefs.GetFloat("stat_secondswasted") + Time.deltaTime);
        

    }


    public void UpdateStat(string stat, float value)
    {
        PlayerPrefs.SetFloat(stat, value);
        statTexts[stat].text = Mathf.Round(PlayerPrefs.GetFloat(stat)).ToString();
    }
}
