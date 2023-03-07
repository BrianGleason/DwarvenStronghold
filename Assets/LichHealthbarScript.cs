using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHealthbarScript : MonoBehaviour
{
    GameObject lich; 
    // Start is called before the first frame update
    void Start()
    {
        lich = GameObject.FindWithTag("Lich");
    }

    // Update is called once per frame
    void Update()
    {
        if (!lich)
        {
            Destroy(gameObject);
        }
    }
}
