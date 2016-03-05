using UnityEngine;
using System.Collections;
using Devdog.InventorySystem;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public string name; // What is the name of the enemy
    public Sprite sprite; // How does the enemy look
    public float hitpoints; // How many hitpoints does the enemy have
    public float health; // How many hitpoints does the enemy have LEFT
    public float damage; //How much damage the enemy can do to the player
    public float xp; //How much xp do I get from this enemy
    public float lootChance; //Percentage of getting loot
    public InventoryItemBase[] loot; //What are the items you can get from this
    public float cash; //How much cash do you get from this monster? (average, get a random around this number)

    public bool dead = false;

    //References
    public Image myImage;
    public Skills skills;
    public AttackScreen attackScreen;
    public Stats stats;

    public GameObject coin;

    void Awake()
    {
        skills = GameObject.Find("Player").GetComponent<Skills>();
        attackScreen = GameObject.Find("Attack Tab").GetComponent<AttackScreen>();
        stats = GameObject.Find("Stats Tab").GetComponent<Stats>();
        myImage = GetComponent<Image>();

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
        GameManager.instance.ShowTextFade("+" + xp + " xp");

        
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
            GetXP();
        }
    }

    int WeaponPower()
    {
        if (GameManager.instance.development) return 2;


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

        skills.skills[0].GetXP(xp);
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
