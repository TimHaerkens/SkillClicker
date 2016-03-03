using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Devdog.InventorySystem.UI;


public class Travel : MonoBehaviour {

    public float travelDistance = 100;
    public float stepsLeft = 100;

    public GameObject destination;
    public GameObject departure;

    //References
    public Text stepsCounter;
    public GameObject pawn;
    public Player player;
    public UIWindowPage areaTab;
    public AreaTab AreaTab;
    public Map map;
    public Gathering gathering;
    public GameObject leftFoot;
    public GameObject rightFoot;
    public Text fromText;
    public Text toText;

    public bool stepToggle = true;

    //Resources

    void Awake()
    {
        gathering = GameManager.instance.GetComponent<Gathering>();
    }

    public void Step()
    {
        //Do a step
        stepsLeft--;
        stepsCounter.text = stepsLeft.ToString();
        float progress = stepsLeft / travelDistance;
        Debug.Log(progress);
        pawn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-355+(710* (1-progress)), 114.5f);
        if(stepToggle)
        {
            stepToggle = false;
            leftFoot.GetComponent<Animator>().Play("leftFootstep");
        }
        else
        {
            stepToggle = true;
            rightFoot.GetComponent<Animator>().Play("rightFootstep");
        }

        //Destination reached?
        if(stepsLeft==0)
        {
            Arrive(destination);
        }

        //Check what you come across



    }


    void Arrive(GameObject dest)
    {
        player.currentArea = dest;
        player.traveling = false;
        AreaTab.UpdateAreaTab();
        map.MovePawn(dest);

        //Reset Travel tab
        pawn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-355, 114.5f);

        //Spawn resources for this area
        gathering.SpawnResources();


        areaTab.Show();

    }
}
