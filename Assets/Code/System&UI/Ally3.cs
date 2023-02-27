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
        int cost = 80 + 5 * (FindObjectsOfType<Dwarf>().Length + FindObjectsOfType<HealerDwarf>().Length + FindObjectsOfType<BerserkerDwarf>().Length);
        if (FindObjectOfType<AllyPlacement>().previewing)
        {
            cost -= 10;
        }
        Text.text = "Berserker (3)\nCost: $" + cost.ToString();
        Text.fontSize = 20;
    }
}
