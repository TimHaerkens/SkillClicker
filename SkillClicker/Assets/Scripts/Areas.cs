using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Areas : MonoBehaviour {

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
    public class Area
    {
        public string name; //What is the name of the village

        public Resource[] resources;

        public Dictionary<string,float> neighbours = new Dictionary<string,float>();

        public Area(string n, Resource[] r)
        {
            name = n;
            resources = r;
        }

    }



    public Area[] areas;




    public Area currentArea;

    void Awake()
    {
        currentArea = areas[0];
    }





}
