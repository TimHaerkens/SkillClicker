using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

    public GameObject characterPawn;

    public void MovePawn(GameObject area)
    {
        characterPawn.GetComponent<RectTransform>().anchoredPosition = area.GetComponent<RectTransform>().anchoredPosition - new Vector2(0, 45);
    }

}
