using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public TextMesh Text;
    public GameObject Base;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMesh>();
        Base = GameObject.FindWithTag("Base");
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Gold: $" + SystemControl.instance.gold.ToString()
            + "\nBase HP: " + Base.GetComponent<Health>().hpPercent()*100 + "%";
        Text.fontSize = 14;

    }
}
