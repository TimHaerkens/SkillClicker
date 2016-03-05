using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

    //reference
    public Text statText;
    public Text value;

    //info
    public string name;
    public string pref;

    public void UpdateValue()
    {
        value.text = PlayerPrefs.GetFloat(pref).ToString();
        statText.text = name;

    }



}
