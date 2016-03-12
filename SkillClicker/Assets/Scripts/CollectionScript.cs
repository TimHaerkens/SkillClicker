using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Devdog.InventorySystem.UI;


public class CollectionScript : MonoBehaviour {

    //My info
    public Gathering.Collection myInfo;

    //References
    public Image icon;
    public Skills skills;

    void Awake()
    {
        skills = GameObject.Find("Player").GetComponent<Skills>();
    }

    public void UpdateIcon()
    {
        icon.sprite = myInfo.icon;
        GetComponent<RectTransform>().localScale = new Vector3(1, 1, 0);
    }

    

    public UIWindowPage GatherTab;

    public void OpenGatherableWindow()
    {
        GatherTab = GameObject.Find("Gathering Tab").GetComponent<UIWindowPage>();
        GatherTab.Show();

        //Choose random of this collection
        List<Gathering.Gatherable> possibleSpawns = new List<Gathering.Gatherable>();
        foreach(Gathering.Gatherable g in myInfo.gatherables)
        {
            float level = 0;
            foreach(Skills.Skill s in skills.skills)
            {
                Debug.Log("s.name: " + s.name + "& g.name: " + g.skill);
                if (s.name == g.skill)
                {
                    Debug.Log("gevonden level: " + s.level);
                    level = s.level;
                }
            }
            if (g.level <= level)
            {
                possibleSpawns.Add(g);
                Debug.Log(g.name + " is viable");
            }
        }

        Gathering.Gatherable spawnInfo = possibleSpawns[Random.Range(0, possibleSpawns.Count)];
        Debug.Log("choice: " + spawnInfo.name);

        GatherTab.GetComponent<GatheringScreen>().info = spawnInfo;
        GatherTab.GetComponent<GatheringScreen>().info2 = myInfo;
        GatherTab.GetComponent<GatheringScreen>().SetClickable();
        GatherTab.GetComponent<GatheringScreen>().clickable.GetComponent<Clicks>().UpdateAmount();

    }
}
