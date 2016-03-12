using UnityEngine;
using System.Collections;
using Devdog.InventorySystem;
using UnityEngine.UI;
using Devdog.InventorySystem.Models;

public class Enemy : MonoBehaviour {

    public string name; // What is the name of the enemy
    public Sprite sprite; // How does the enemy look
    public int level;
    public float hitpoints; // How many hitpoints does the enemy have
    public float health; // How many hitpoints does the enemy have LEFT
    public float damage; //How much damage the enemy can do to the player
    public float lootChance; //Percentage of getting loot
    public InventoryItemBase[] loot; //What are the items you can get from this
    public float cash; //How much cash do you get from this monster? (average, get a random around this number)

    public bool dead = false;

    //References
    public Image myImage;
    public Skills skills;
    public AttackScreen attackScreen;
    public Stats stats;
    public CharacterUI characterUI;


    public GameObject coin;

    public int Hitpoints()
    {
        float baseClicks = 5;
        //Debug.Log("You must click " + (baseClicks + (level * Mathf.Log(level, 1.21f))) + " times for " + name);
        return Mathf.RoundToInt(baseClicks + (level * Mathf.Log(level, 1.21f)));
    }

    public float xp()
    {
        float baseXP = 3;
        Debug.Log("You get " + (baseXP + (level * Mathf.Log(level, 1.9f))) + " xp");
        return baseXP + (level * Mathf.Log(level, 1.9f));
    }

    void Awake()
    {
        skills = GameObject.Find("Player").GetComponent<Skills>();
        attackScreen = GameObject.Find("Attack Tab").GetComponent<AttackScreen>();
        characterUI = GameObject.Find("Character Tab").GetComponent<CharacterUI>();
        stats = GameObject.Find("Stats Tab").GetComponent<Stats>();
        myImage = GetComponent<Image>();
        hitpoints = Hitpoints();


        health = hitpoints;


        //Update attack screen
        attackScreen.name.text = name;
        UpdateHealthBar(health / hitpoints);
        attackScreen.UpdateXPBar(skills.skills[0].LevelProgress());


        GetComponent<Button>().onClick.AddListener(() => { Clicked();  });


    }

  

    //When the enemy dies
    public void Die()
    {
        Stats.instance.UpdateStat("stat_monsterskilled", PlayerPrefs.GetFloat("stat_monsterskilled") + 1);
        dead = true;
        float cashAmount = Random.Range(cash - (cash / 10), cash + (cash / 10));
        for(int i = 0; i < cashAmount; i++)
        {
            GameObject coinSpawn = Instantiate(coin, transform.position, Quaternion.identity) as GameObject;
            coinSpawn.transform.parent = attackScreen.gameObject.transform;
            //coinSpawn.transform.SetAsFirstSibling();
            coinSpawn.transform.localScale = new Vector3(1, 1, 1);
        }
        myImage.color = Color.grey;
        GameManager.instance.ShowTextFade("+" + xp() + " xp");

        
    }

    float deadTimer = 0;
    void Update()
    {
        if(dead)
        {
            deadTimer += Time.deltaTime;
            if (deadTimer>0.5f)
            {
                attackScreen.SpawnEnemy();
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Rotate(0, Input.gyro.rotationRateUnbiased.y, 0);
        }
    }

    public void Clicked()
    {
        if (dead) return;
        //FMODUnity.RuntimeManager.PlayOneShot("event:/"+info.clickSound, transform.position);
        //stats.UpdateStat("stat_timesclicked", PlayerPrefs.GetFloat("stat_timesclicked") + 1);
        health -= WeaponPower(); //TODO: Calculate damage
        UpdateHealthBar(health/hitpoints);
        if (health <= 0)
        {
            Die();
            Loot(WhatLoot());
            GetXP();
        }
    }

    public GameObject loot_pfb;//Prefab of drop
    void Loot(InventoryItemBase whatLoot)
    {
        GameObject lootSpawn = Instantiate(loot_pfb, transform.position, Quaternion.identity) as GameObject;
        lootSpawn.transform.parent = attackScreen.gameObject.transform;
        lootSpawn.transform.SetAsLastSibling();
        lootSpawn.transform.localScale = new Vector3(1, 1, 1);
        lootSpawn.GetComponent<Loot>().loot = whatLoot;
        lootSpawn.GetComponent<Loot>().image.sprite = whatLoot.icon;

        //FMODUnity.RuntimeManager.PlayOneShot("event:/succes", transform.position);


    }

    int WeaponPower()
    {
        float tempWeaponpower = 1;

        //Calculate tool power
        float tl = GetToolLevel();
        float sl = GetSkillLevel();

        tempWeaponpower = (tl * (Mathf.Pow(1.01f, tl))) + ((sl * 0.2f));

        Debug.Log(tl + " + " + sl * 0.2f + " = " + Mathf.FloorToInt(tempWeaponpower));

        if (GameManager.instance.development) return Mathf.FloorToInt(tempWeaponpower * 2);

        return Mathf.FloorToInt(tempWeaponpower);
    }

    float GetToolLevel()
    {
        
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
                    if (pr.value != "Attack")
                    {
                        toolFound = false;
                    }
                    else
                    {
                        toolFound = true;
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

        if (tl == 0) return 1;

        return tl;
    }

    float GetSkillLevel()
    {
        string skill = "Attack";
        foreach (Skills.Skill s in GameObject.Find("Player").GetComponent<Skills>().skills)
        {
            if (s.name == skill)
            {
                return s.level;
            }
        }
        return 1;
    }

    void UpdateHealthBar(float progress)
    {
        attackScreen.enemyHP.anchoredPosition = new Vector2(750 * progress / 2, 0);
        attackScreen.enemyHP.sizeDelta = new Vector2(750 * progress, 96);
    }

    

    void GetXP()
    {
        float progress = 0;

        skills.skills[0].GetXP(xp());
        progress = skills.skills[0].LevelProgress();
        skills.UpdateSkillsInfo("attack", skills.skills[0].xp);

        attackScreen.UpdateXPBar(progress);
    }

    InventoryItemBase WhatLoot()
    {
        InventoryItemBase whatLoot = new InventoryItemBase();

        whatLoot = loot[Random.Range(0, loot.Length)];

        return whatLoot;
    }

}
