using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally2 : MonoBehaviour
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
        int cost = 20 + 5 * (FindObjectsOfType<Dwarf>().Length + FindObjectsOfType<HealerDwarf>().Length + FindObjectsOfType<BerserkerDwarf>().Length);
        if (FindObjectOfType<AllyPlacement>().previewing)
        {
            cost -= 10;
        }
        Text.text = "Healer (2)\nCost: $" + cost.ToString();
        Text.fontSize = 20;
    }
}
