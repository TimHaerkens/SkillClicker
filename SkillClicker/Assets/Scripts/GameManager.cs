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
    public UIWindow loadingScreen;
    public RectTransform loadingBar;

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
        loadingBar = GameObject.Find("LoadingBar").GetComponent<RectTransform>();



        LoadEverything();

    }

    void SaveEverything()
    {
        Debug.Log("Saving Everything");
        //Open all tabs to save them
        GameObject.Find("Inventory Tab").GetComponent<UIWindowPage>().Show();
        GameObject.Find("Inventory Tab").transform.FindChild("InvPanel").GetComponent<EasySave2CollectionSaverLoader>().Save();
        GameObject.Find("Character Tab").GetComponent<UIWindowPage>().Show();
        GameObject.Find("Character Tab").GetComponent<EasySave2CollectionSaverLoader>().Save();
        GameObject.Find("Bank Tab").GetComponent<UIWindowPage>().Show();
        GameObject.Find("Area Tab").GetComponent<UIWindowPage>().Show();
    }

    void LoadEverything()
    {
        Debug.Log("Loading Everything");
        StartCoroutine(Loading()); 
    }

    int step = 0;
    IEnumerator Loading()
    {
        yield return new WaitForSeconds(0.2f);

        //Open all tabs to load them
        //Debug.Log("Doing step " + step);
        switch (step)
        {
            case 0:
                GameObject.Find("Inventory Tab").GetComponent<UIWindowPage>().Show();
            break;
            case 1:
                GameObject.Find("Character Tab").GetComponent<UIWindowPage>().Show();
            break;
            case 2:
                GameObject.Find("Bank Tab").GetComponent<UIWindowPage>().Show();
            break;
            case 3:
                GameObject.Find("Area Tab").GetComponent<UIWindowPage>().Show();
            break;
            case 4:
                loadingScreen.Hide();
            break;
        }

        if (step < 4)
        {
            step++;
            StartCoroutine(Loading());
        }
    }

    void OnApplicationQuit()
    {
        SaveEverything();
    }

    

    bool celebrationActive = false;
    float celebrationTimer = 0;

    void Update()
    {
        if (notificationCooldown > 0) notificationCooldown -= Time.deltaTime;
        if (notificationCooldown < 0) notificationCooldown = 0;

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha4))
            InventoryManager.AddCurrency(1, 2);
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha5))
            InventoryManager.AddCurrency(-1, 2);

        if (celebrationActive)
        {
            celebrationTimer += Time.deltaTime;
            celebration.color = new Color(1, 1, 1, Mathf.Lerp(celebration.color.a, 1, celebrationTimer));
            if(celebrationTimer > 3) celebration.color = new Color(1, 1, 1, Mathf.Lerp(celebration.color.a, 0, celebrationTimer-3));
            if (celebrationTimer > 4)
            {
                celebrationTimer = 0;
                celebrationActive = false;
            }
        }

        loadingBar.anchoredPosition = new Vector2(-200+(50* step), -189);
        loadingBar.sizeDelta = new Vector2(100*step,22);


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
            newNotification.SetAsLastSibling();
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
        newNotification.SetAsLastSibling();
        
    }

    public void Celebration()
    {
        celebrationActive = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetSkills()
    {
        Skills skills = GameObject.Find("Player").GetComponent<Skills>();
        foreach (Skills.Skill s in skills.skills)
        {
            s.Reset();
        }
    }
}
