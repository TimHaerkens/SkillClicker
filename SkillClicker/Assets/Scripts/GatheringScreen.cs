using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GatheringScreen : MonoBehaviour
{
    public Gathering.Gatherable info;

    //Images
    public Sprite[] skillIcons;

    //References
    public Text name;
    public GameObject clickable;
    public GameObject skill;

    public void SetClickable()
    {
        name.text = info.name;
        clickable.GetComponent<Image>().sprite = info.image;

        if (info.skill == "Woodcutting") skill.GetComponent<Image>().sprite = skillIcons[0];
        if (info.skill == "Mining") skill.GetComponent<Image>().sprite = skillIcons[1];
        if (info.skill == "Gathering") skill.GetComponent<Image>().sprite = skillIcons[2];

        clickable.GetComponent<Clicks>().info = info;
        

    }

}
