using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AttackScreen : MonoBehaviour
{
    public Skills skills;

    //Images
    public Sprite[] skillIcons;

    //References
    public Player player;
    public Text name;
    public GameObject clickable;
    public GameObject skill;
    public RectTransform xpBar;
    public RectTransform enemyHP;

    //Slot
    public GameObject currentEnemy; 

    void Awake()
    {
    }

   
    public void SpawnEnemy()
    {
        if(currentEnemy!=null)Destroy(currentEnemy);

        List<GameObject> draw = new List<GameObject>();
        foreach(GameObject possibleEnemy in player.currentArea.GetComponent<Area>().enemies)
        {
            if (possibleEnemy.GetComponent<Enemy>().level <= skills.skills[0].level + 1) draw.Add(possibleEnemy);
        }
        GameObject chosenEnemy = draw[Random.Range(0, draw.Count)];
        GameObject newEnemy = Instantiate(chosenEnemy, chosenEnemy.transform.position, Quaternion.identity) as GameObject;
        newEnemy.transform.parent = this.gameObject.transform;
        newEnemy.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        name.text = chosenEnemy.GetComponent<Enemy>().name;

        currentEnemy = newEnemy;
    }

    public void UpdateXPBar(float progress)
    {
        xpBar.anchoredPosition = new Vector2(500 * (progress / 100) / 2, 0);
        xpBar.sizeDelta = new Vector2(500 * (progress / 100), 20);
    }



}
