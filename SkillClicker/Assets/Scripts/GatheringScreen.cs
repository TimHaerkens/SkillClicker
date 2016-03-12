using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Devdog.InventorySystem;

public class GatheringScreen : MonoBehaviour
{
    public Gathering.Gatherable info;
    public Gathering.Collection info2;
    public Skills skills;

    //Images
    public Sprite[] skillIcons;

    //Buttons
    public List<Button> ingredientButtons = new List<Button>();

    //References
    public Text name;
    public GameObject clickable;
    public GameObject skill;
    public RectTransform xpBar;
    public GameObject ingredientsPanel;

    //Resources
    public Button ingredientButton;

    public void SetClickable()
    {
        //Remove ingredients
        foreach (Button b in ingredientButtons) Destroy(b.gameObject);
        ingredientButtons.Clear();

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
            UpdateXPBar(skills.skills[3].LevelProgress());
        }
        if (info.skill == "Crafting")
        {
            skill.GetComponent<Image>().sprite = skillIcons[4];
            UpdateXPBar(skills.skills[6].LevelProgress());
            ShowIngredients();
            
        }
        if (info.skill == "Cooking")
        {
            skill.GetComponent<Image>().sprite = skillIcons[5];
            UpdateXPBar(skills.skills[7].LevelProgress());
            ShowIngredients();
        }

        clickable.GetComponent<Clicks>().info = info;
        clickable.GetComponent<Clicks>().info2 = info2;



    }

    void ShowIngredients()
    {
        //Show ingredients
        foreach (Gathering.Gatherable.ingredientData id in info.ingredients)
        {
            Button newIngredient = Instantiate(ingredientButton, ingredientButton.transform.position, Quaternion.identity) as Button;
            newIngredient.transform.parent = ingredientsPanel.transform;
            newIngredient.transform.localScale = new Vector2(1, 1);
            //Calculate how much of the item you have
            var allOfID = InventoryManager.FindAll(id.ingredient.ID, false);
            uint total = 0;
            foreach (InventoryItemBase item in allOfID)
            {
                total += item.currentStackSize;
            }
            newIngredient.transform.FindChild("Amount").GetComponent<Text>().text = total + "/" + id.amount;
            newIngredient.transform.FindChild("Image").GetComponent<Image>().sprite = id.ingredient.icon;
            ingredientButtons.Add(newIngredient);
        }
    }

    public void UpdateXPBar(float progress)
    {
        xpBar.anchoredPosition = new Vector2(500 * (progress / 100) / 2, 0);
        xpBar.sizeDelta = new Vector2(500 * (progress / 100), 20);
    }



}
