using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {

	public void DeleteMe()
    {
        Destroy(transform.parent.gameObject);
    }
}
