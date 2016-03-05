using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;
using Devdog.InventorySystem.UI;
using Devdog.InventorySystem.Integration.EasySave2;

public class GameManager : MonoBehaviour {

    public bool development = false;

    //Reference
    public Image celebration;

    private static GameManager Instance;

    public static GameManager instance
    {
        get
        {
            if (Instance == null)
                Instance = GameObject.FindObjectOfType<GameManager>();
            return Instance;
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            if (this != Instance)
                Destroy(this.gameObject);
        }

        Input.gyro.enabled = true;



    }

    void OnApplicationQuit()
    {
        //Open all tabs to save them
        GameObject.Find("Inventory Tab").GetComponent<UIWindowPage>().Show();
        GameObject.Find("Character Tab").GetComponent<UIWindowPage>().Show();
        GameObject.Find("Bank Tab").GetComponent<UIWindowPage>().Show();
        
        
        //GameObject.Find("Inventory Tab").transform.FindChild("InvPanel").GetComponent<EasySave2CollectionSaverLoader>().Save();

    }

    bool celebrationActive = false;
    float celebrationTimer = 0;

    void Update()
    {
        if (notificationCooldown > 0) notificationCooldown -= Time.deltaTime;
        if (notificationCooldown < 0) notificationCooldown = 0;


        if(celebrationActive)
        {
            celebrationTimer += Time.deltaTime;
            celebration.color = new Color(1, 1, 1, Mathf.Lerp(celebration.color.a, 1, celebrationTimer));
            if(celebrationTimer > 3) celebration.color = new Color(1, 1, 1, Mathf.Lerp(celebration.color.a, 0, celebrationTimer-3));
            if (celebrationTimer > 4)
            {
                Debug.Log("Disable");
                celebrationTimer = 0;
                celebrationActive = false;
            }
        }


    }


    public RectTransform notification;
    public GameObject contentPanel;
    float notificationCooldown = 0;

    public void ShowNotification(string message)
    {
        if (notificationCooldown <= 0)
        {
            GameObject anyTextFade = GameObject.Find("TextFade");
            if (anyTextFade != null) Destroy(anyTextFade);
            notificationCooldown = 1;
            RectTransform newNotification = Instantiate(notification, notification.transform.position, Quaternion.identity) as RectTransform;
            newNotification.SetParent(contentPanel.transform);
            newNotification.transform.localScale = new Vector2(1, 1);
            newNotification.anchoredPosition = new Vector2(0, 0);
            newNotification.sizeDelta = new Vector2(0, 0);
            newNotification.FindChild("Text").GetComponent<Text>().text = message;
            newNotification.FindChild("Text").GetComponent<Animator>().Play("notification");
            newNotification.SetAsFirstSibling();
        }
    }

    public void ShowTextFade(string message)
    {
        
        RectTransform newNotification = Instantiate(notification, notification.transform.position, Quaternion.identity) as RectTransform;
        newNotification.gameObject.name = "TextFade";
        newNotification.SetParent(contentPanel.transform);
        newNotification.transform.localScale = new Vector2(1, 1);
        newNotification.anchoredPosition = new Vector2(0, 0);
        newNotification.sizeDelta = new Vector2(0, 0);
        newNotification.FindChild("Text").GetComponent<Text>().text = message;
        newNotification.FindChild("Text").GetComponent<Animator>().Play("textFade");
        newNotification.SetAsFirstSibling();
        
    }

    public void Celebration()
    {
        celebrationActive = true;
    }
}
