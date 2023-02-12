using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally3 : MonoBehaviour
{
    public TextMesh Text;
    public AllyPlacement ally;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMesh>();
        ally = FindObjectOfType<AllyPlacement>();
    }

    // Update is called once per frame
    void Update()
    {
        Text.text = "Ranged Dwarf, Cost: $4\nRemaining Cooldown: "
            + Mathf.Round((ally.cooldowns[2] * 100.0f) / 100.0f).ToString();
    }
}