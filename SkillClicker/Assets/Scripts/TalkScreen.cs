using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TalkScreen : MonoBehaviour {

    public Text name;
    public Image image;
    public GameObject optionsPanel;

    public NPC currentNPC;

    //Resources
    public Button shopButton;

    public List<Button> buttons = new List<Button>();
    public void SetTalkInfo()
    {
        //Set info on Talk Screen
        name.text = currentNPC.name;
        image.sprite = currentNPC.sprite;

        foreach (Button b in buttons) Destroy(b.gameObject);
        buttons.Clear();
        //Spawning buttons
        if (currentNPC.hasShop)
        {
            Debug.Log("test");
            Button newButton = Instantiate(shopButton, shopButton.transform.position, Quaternion.identity) as Button;
            newButton.transform.parent = optionsPanel.transform;
            newButton.transform.localScale = new Vector2(1, 1);
            newButton.GetComponent<OpenShop>().npc = currentNPC;
            buttons.Add(newButton);
        }
    }


}
