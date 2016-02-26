using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Clickbar : MonoBehaviour {

    public Scrollbar clickbar;
    public float clicks = 100;

    public void Damaga(float value)
    {
        clicks -= value;
        clickbar.size = clicks/100f;
    }

	
}
