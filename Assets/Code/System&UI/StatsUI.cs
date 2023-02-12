using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public TextMesh Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Gold: $" + SystemControl.instance.gold.ToString() + "\n";
    }
}
