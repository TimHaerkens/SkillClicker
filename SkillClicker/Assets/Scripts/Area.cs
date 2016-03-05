using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Devdog.InventorySystem.UI;
using Devdog.InventorySystem;

public class Area : MonoBehaviour {

    [System.Serializable]
    public class Resource
    {
        public string type;
        public int id;

        public Resource(string t, int i)
        {
            type = t;
            id = i;
        }

    }

    [System.Serializable]
    public class Recipe
    {
        public string type;
        public int id;

        public Recipe(string t, int i)
        {
            type = t;
            id = i;
        }

    }
    [System.Serializable]
    public class Blueprint
    {
        public string type;
        public int id;

        public Blueprint(string t, int i)
        {
            type = t;
            id = i;
        }

    }
    [System.Serializable]
    public class Neighbour
    {
        public string name;
        public GameObject area;
        public float distance;
        public GameObject road;

        public Neighbour(string n, GameObject a, float d, GameObject r)
        {
            name = n;
            area = a;
            distance = d;
            road = r;
        }
    }

   

    public string name; //What is the name of the village
    public Resource[] resources; //What resources does this village have
    public Resource[] recipes; //What recipes does this village have
    public Resource[] blueprints; //What blueprints does this village have
    public Neighbour[] neighbours; //What areas can I go to from here?
    public GameObject[] enemies; //What enemies are in this area
    public NPC[] npcs; //What npcs are in this area

    //Reference
    public Player player;
    public UIWindowPage TravelTab;
    public Map map;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        TravelTab = GameObject.Find("Travel Tab").GetComponent<UIWindowPage>();
        map = GameObject.Find("Map Tab").GetComponent<Map>();

        if (player.currentArea == this.gameObject)
        {
            map.MovePawn(this.gameObject);
        }


    }



    public void Travel()
    {
        //Travel to this city
        GameObject from = player.currentArea;
        GameObject to = gameObject;

        if (from == to) return;

        Debug.Log("Travel from " + from.name + " to " + to.name);
        //GameManager.instance.ShowNotification("Travel from " + from.name + " to " + to.name);

        //Check if you can travel there
        bool canGo = false;
        float distance = 0;
        foreach(Neighbour neighbour in from.GetComponent<Area>().neighbours)
        {
            Debug.Log(neighbour.name + " = " + to.name + "?");
            if(neighbour.area == to)
            {
                canGo = true;
                neighbour.road.GetComponent<Animator>().Play("roadblink");
                distance = neighbour.distance;
            }
        }
        if(canGo)
        {
            player.traveling = true;
            TravelTab.Show();
            TravelTab.GetComponent<Travel>().departure = from;
            TravelTab.GetComponent<Travel>().destination = to;
            TravelTab.GetComponent<Travel>().travelDistance = distance;
            TravelTab.GetComponent<Travel>().stepsLeft = distance;
            TravelTab.GetComponent<Travel>().stepsCounter.text = distance.ToString();
            TravelTab.GetComponent<Travel>().fromText.text = from.name;
            TravelTab.GetComponent<Travel>().toText.text = to.name;

        }

    }

}
