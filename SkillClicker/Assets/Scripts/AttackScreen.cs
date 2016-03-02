using UnityEngine;
using System.Collections;
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

    void Awake()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        GameObject[] enemyDraw = player.currentArea.GetComponent<Area>().enemies;
        GameObject chosenEnemy = enemyDraw[Random.Range(0, enemyDraw.Length)];
        GameObject newEnemy = Instantiate(chosenEnemy, chosenEnemy.transform.position, Quaternion.identity) as GameObject;
        newEnemy.transform.parent = this.gameObject.transform;
        newEnemy.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        newEnemy.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }

    public void UpdateXPBar(float progress)
    {
        xpBar.anchoredPosition = new Vector2(500 * (progress / 100) / 2, 0);
        xpBar.sizeDelta = new Vector2(500 * (progress / 100), 20);
    }



}
