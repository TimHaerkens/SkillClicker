using UnityEngine;
using System.Collections;
using Devdog.InventorySystem;

public class Coin : MonoBehaviour {


    float distance = 0.8f;
    public void Awake()
    {

        directionX = Random.Range(-1.0f, 1.0f);
        directionY = Mathf.Sqrt(Mathf.Abs((distance * distance) - (directionX * directionX)));
        if (Random.Range(1, 3) == 2) directionY *= -1;
        Debug.Log(directionY);
    }

    float timer = 0;
    void Update()
    {
        Move();
        timer += Time.deltaTime;
        if(timer>3)
        {
            Get();
        }
    }

    float directionX = 0;
    float directionY = 0;
    float speed = 1500;
    float increasedDecrease = 1;
    void Move()
    {
        increasedDecrease += Time.deltaTime*2;
        if(speed > 0)speed -= Time.deltaTime*1800*increasedDecrease;
        if (speed < 0) speed = 0;

        GetComponent<RectTransform>().anchoredPosition += new Vector2(speed * directionX * Time.deltaTime, speed * directionY*Time.deltaTime);
    }

	public void Get()
    {
        InventoryManager.AddCurrency(1, 2); //If cash is 50, get between 45 and 55 cash
        Stats.instance.UpdateStat("stat_moneyearned", PlayerPrefs.GetFloat("stat_moneyearned") + 1);
        Destroy(gameObject);
    }
}
