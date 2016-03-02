using UnityEngine;
using System.Collections;
using Devdog.InventorySystem.UI;

public class NPCButton : MonoBehaviour {

    public NPC npc;
    TalkScreen talkScreen;

    void Awake()
    {
        talkScreen = GameObject.Find("Talk Tab").GetComponent<TalkScreen>();
    }

	public void GoTalk()
    {
        talkScreen.currentNPC = npc;
        talkScreen.GetComponent<UIWindowPage>().Show();
    }
}
