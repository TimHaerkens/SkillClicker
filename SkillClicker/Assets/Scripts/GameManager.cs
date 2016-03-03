using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem;

public class GameManager : MonoBehaviour {

    public bool development = false;

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

        

    }



    void Update()
    {
        

    }


    public RectTransform notification;
    public GameObject contentPanel;

    public void ShowNotification(string message)
    {
        RectTransform newNotification = Instantiate(notification, notification.transform.position, Quaternion.identity) as RectTransform;
        newNotification.SetParent(contentPanel.transform);
        newNotification.transform.localScale = new Vector2(1, 1);
        newNotification.anchoredPosition = new Vector2(0, 0);
        newNotification.sizeDelta = new Vector2(0, 0);
        newNotification.FindChild("Text").GetComponent<Text>().text = message;
        newNotification.SetAsFirstSibling();

    }
}
