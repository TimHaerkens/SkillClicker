using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GatheringScreen : MonoBehaviour
{
    public Gathering.Gatherable info;
    public Skills skills;

    //Images
    public Sprite[] skillIcons;

    //References
    public Text name;
    public GameObject clickable;
    public GameObject skill;
    public RectTransform xpBar;

    public void SetClickable()
    {
        name.text = info.name;
        clickable.GetComponent<Image>().sprite = info.image;

        if (info.skill == "Woodcutting")
        {
            skill.GetComponent<Image>().sprite = skillIcons[0];
            UpdateXPBar(skills.skills[2].LevelProgress());
        }
        if (info.skill == "Mining")
        {
            skill.GetComponent<Image>().sprite = skillIcons[1];
            UpdateXPBar(skills.skills[1].LevelProgress());
        }
        if (info.skill == "Gathering")
        {
            skill.GetComponent<Image>().sprite = skillIcons[2];
            UpdateXPBar(skills.skills[4].LevelProgress());
        }
        if (info.skill == "Fishing")
        {
            skill.GetComponent<Image>().sprite = skillIcons[3];
            UpdateXPBar(skills.skills[5].LevelProgress());
        }
        if (info.skill == "Crafting")
        {
            skill.GetComponent<Image>().sprite = skillIcons[4];
            UpdateXPBar(skills.skills[6].LevelProgress());
        }
        if (info.skill == "Cooking")
        {
            skill.GetComponent<Image>().sprite = skillIcons[5];
            UpdateXPBar(skills.skills[7].LevelProgress());
        }

        clickable.GetComponent<Clicks>().info = info;

       

    }

    public void UpdateXPBar(float progress)
    {
        xpBar.anchoredPosition = new Vector2(500 * (progress / 100) / 2, 0);
        xpBar.sizeDelta = new Vector2(500 * (progress / 100), 20);
    }



}
