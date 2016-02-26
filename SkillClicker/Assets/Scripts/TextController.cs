using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {
	
	public Text text;
	private enum States {intro, Aa, Ab, Ac, Ad, Aaa, Aab, Aaaa, Aaab, Aaaaa, Aaaab, Ada, Adb, B, Ba, Bb, Quest_1};
	private States myState;
	// Use this for initialization

	void Start () {
		myState = States.intro;
	}
	// Update is called once per frame
	void Update () {
		print (myState);
		if (myState == States.intro) {state_intro();}
		else if (myState == States.Aa) {state_Aa();}
		else if (myState == States.Ab) {state_Ab();}
		else if (myState == States.Ac) {state_Ac();}
		else if (myState == States.Ad) {state_Ad();}
        else if (myState == States.Aaa) { state_Aaa();}
        else if (myState == States.Aab) { state_Aab(); }
        else if (myState == States.Aaaa) { state_Aaaa(); }
        else if (myState == States.Aaab) { state_Aaab(); }
        else if (myState == States.Aaaaa) { state_Aaaaa(); }
        else if (myState == States.Aaaab) { state_Aaaab(); }
        else if (myState == States.Ada) {state_Ada();}
		else if (myState == States.Adb) {state_Adb();}
        else if (myState == States.Quest_1) { state_Quest_1(); }
    }
	void state_intro() {
		text.text = "You wake up in a small bed. The sun is shining through a window and you feel the warmth on your skin. \n" +
"\"Where am I?\" you are wondering. \"What is happened?\" You remember nothing and then you see some marks on your hands. \n" +
"\"Ah! You are finally awake!\" You turn around and see a old man walking into the room. Doesn't look like a doctor to you. \n" +
"\"How are you feeling?\" You answer with..\n" +
"\n" +
"A.	\"Fine.\n" +
"B.	\"Couldn't be better.\n" +
"C.	\"What do you care?\n" +
"D.	..." ;
		if (Input.GetKeyDown(KeyCode.A)) {myState = States.Aa;}
		else if (Input.GetKeyDown(KeyCode.B)) {myState = States.Ab;}
		else if (Input.GetKeyDown(KeyCode.C)) {myState = States.Ac;}
        else if (Input.GetKeyDown(KeyCode.D)) { myState = States.Ad; }
    }
	void state_Aa() {
		text.text = "Good to hear! It was quite the storm last week. You are lucky to be alive. \n" +
        "\"Storm? Last 	week?\" you ask. \"Oh yes, you stranded on the beach. I found you the other morning between the wrecked wood and other bits of your ship I presume. \n" +
        "\"My ship? I can't remember anything. \"I believe so, the water and cold isn't good for anyone. As well as the wound on you head,but I healed for it you.\n" +
        "\n" +
        "A.	\"Thank you.\n" +
        "B.	\"Do I owe you something?\n";
		if (Input.GetKeyDown(KeyCode.A)) {myState = States.Aaa;}
		else if (Input.GetKeyDown(KeyCode.B)) {myState = States.Aab;}
	}
	void state_Ab() {
		text.text = "I'm glad my healing skills are doing just fine then on you. \n" +
        "It was quite the storm last week. You are lucky to be alive. \n" +
        "\"Storm? Last 	week?\" you ask. \"Oh yes, you stranded on the beach. I found you the other morning between the wrecked wood and other bits of your ship I presume. \n" +
        "\"My ship? I can't remember anything. \"I believe so, the water and cold isn't good for anyone. As well as the wound on you head,but I healed for it you.\n" +
        "\n" +
        "A.	\"Thank you.\n" +
        "B.	\"Do I owe you something?\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aab; }
    }
	void state_Ac() {
		text.text = "Why the grumpy face? I care about anyone in need, even you. \n" +
        "It was quite the storm last week. You are lucky to be alive. \n" +
        "\"Storm? Last 	week?\" you ask. \"Oh yes, you stranded on the beach. I found you the other morning between the wrecked wood and other bits of your ship I presume. \n" +
        "\"My ship? I can't remember anything. \"I believe so, the water and cold isn't good for anyone. As well as the wound on you head,but I healed for it you.\n" +
        "\n" +
        "A.	\"Thank you.\n" +
        "B.	\"Do I owe you something?\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aab; }
    }
	void state_Ad() {
		text.text = "Can't speak eh? Well there is a problem. Maybe this potion will help you. \n" +
        "The old man gives you an odd looking potion and you take a sip. Instantly you are feeling much better and a bit warm. \n" +
        "The old man continues... \n" +
        "It was quite the storm last week. You are lucky to be alive. \n" +
        "\"Storm? Last 	week?\" you ask. \"Oh yes, you stranded on the beach. I found you the other morning between the wrecked wood and other bits of your ship I presume. \n" +
        "\"My ship? I can't remember anything. \"I believe so, the water and cold isn't good for anyone. As well as the wound on you head,but I healed for it you.\n" +
        "\n" +
        "A.	\"Thank you.\n" +
        "B.	\"Do I owe you something?\n";
        if (Input.GetKeyDown(KeyCode.A)) {myState = States.Ada;}
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Adb; }
    }
    void state_Aaa() {
        text.text = "Always here to help anyone in need. Now enough chit-chatting, what is your name? \n" +
        "\n" +
        "A. \"Type your name here.\n" +
        "B. \"I don't remember.\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaab; }
    }
    void state_Aab()    {
        text.text = "You don't owe me anything. Just happy to help anyone in need. Can you remember your name? \n" +
        "A. \"Type your name here.\n" +
        "B. \"I don't remember.\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaab; }
    }
    void state_Aaaa() {
        text.text = "Good to know you remember something after all. Welcome to plaatsnaam [naam player]. \n" +
        "My name is naam npc. I am just an old man in this fishing town. We used to be a very popular and wealthy city, \n" +
        "but now it is nothing but stinky fish and some stinky locals haha! I think you can help some of the locals around here. \n" +
        "Maybe fixing up your ship again or just train some skills to gain money and buy a ship. Anyway,'a lot to do and so much time' I always say. \n" +
        "A. \"Thank you, I will be going now.\n" +
        "B. \"Thank you, can I help you with something?\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaaaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaaab; }
    }
    void state_Aaab() {
        text.text = "Hmm, maybe we need to come up with a name together then. (name picker) Welcome to plaatsnaam \n" +
        "My name is naam npc. I am just an old man in this fishing town. We used to be a very popular and wealthy city, \n" +
        "but now it is nothing but stinky fish and some stinky locals haha! I think you can help some of the locals around here. \n" +
        "Maybe fixing up your ship again or just train some skills to gain money and buy a ship. Anyway,'a lot to do and so much time' I always say. \n" +
        "A. \"Thank you, I will be going now.\n" +
        "B. \"Thank you, can I help you with something?\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaaaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaaab; }
    }
    void state_Aaaaa()  {
        text.text = "You leave the small house. \n" +
        "Press R for Restart.";
        if (Input.GetKeyDown(KeyCode.R)) { myState = States.intro; }
        else if (Input.GetKeyDown(KeyCode.Q)) { myState = States.intro; }
    }
    void state_Aaaab()  {
        text.text = "Well, now that you are mentioning it. Could you gather sticks and maple logs for my fire? 10 of each shall do the trick. \n" +
        "A. \"Accept Quest.\n" +
        "B. \"Decline Quest and exit house.\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Quest_1; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaaaa; }
    }
    void state_Ada()  {
        text.text = "Good, the potion helped. You can speak again. \n" +
        "I am always here to help anyone in need. Now enough chit-chatting, what is your name? \n" +
        "\n" +
        "A. \"Type your name here.\n" +
        "B. \"I don't remember.\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaab; }
    }
    void state_Adb()  {
        text.text = "Good, the potion helped. You don't owe me anything. \n" +
        "Just happy to help anyone in need. Can you remember your name? \n" +
        "A. \"Type your name here.\n" +
        "B. \"I don't remember.\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.Aaaa; }
        else if (Input.GetKeyDown(KeyCode.B)) { myState = States.Aaab; }
    }
    void state_Quest_1()  {
        text.text = "Thank you! Here is a stone axe to help you on the way. \n" +
        "A. \"Go to town.\n";
        if (Input.GetKeyDown(KeyCode.A)) { myState = States.intro; }
        else if (Input.GetKeyDown(KeyCode.Q)) { myState = States.intro; }
    }
}