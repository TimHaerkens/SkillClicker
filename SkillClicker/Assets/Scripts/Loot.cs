using UnityEngine;
using System.Collections;
using Devdog.InventorySystem;
using UnityEngine.UI;

public class Loot : MonoBehaviour {


    public InventoryItemBase loot;
    public Image image;
    float distance = 0.5f;
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

    public ItemCollectionBase inventory;
    public void Get()
    {
        
        if (GameObject.Find("Player").GetComponent<InventoryPlayer>().inventoryCollections[0].GetEmptySlotsCount() == 0)
        {
            GameManager.instance.ShowNotification("Placed in overflow");
            Destroy(gameObject);
            return;
        }
        InventoryManager.AddItem(loot);
        Destroy(gameObject);
    }
}
