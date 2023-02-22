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
        Text.text = "Healer Dwarf\nCost: $3";
        Text.fontSize = 20;
    }
}
