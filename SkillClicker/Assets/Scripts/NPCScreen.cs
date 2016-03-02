using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NPCScreen : MonoBehaviour {

    public Player player;
    public Button npcButton;

    public void SpawnNPCButtons()
    {
        foreach (Button b in buttons)Destroy(b.gameObject);
        buttons.Clear();
        foreach (NPC npc in player.currentArea.GetComponent<Area>().npcs)
        {
            Spawn(npc.name, npc.buttonSprite, npc);
        }
    }

    public List<Button> buttons = new List<Button>();

    //Spawn a button
    void Spawn(string name, Sprite sprite, NPC npc)
    {
        //GameObject newGatherable = Instantiate(gatherableIcon, gatherableIcon.transform.position, Quaternion.identity) as GameObject;
        Button newButton = Instantiate(npcButton, npcButton.transform.position, Quaternion.identity) as Button;
        newButton.transform.parent = this.transform.FindChild("NPCs").transform;
        newButton.transform.localScale = new Vector2(1, 1);
        newButton.GetComponent<Image>().sprite = sprite;
        newButton.GetComponent<NPCButton>().npc = npc;
        buttons.Add(newButton);
    }
}
