using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AreaTab : MonoBehaviour {

    public GameObject currentArea;
    public Text name;

    //reference
    public Player player;

	public void UpdateAreaTab()
    {
        currentArea = player.currentArea;
        name.text = currentArea.name;
    }
}
