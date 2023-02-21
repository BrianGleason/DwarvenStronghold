using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private Image panel;

    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<Image>();
        panel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                panel.enabled = false;
                Time.timeScale = 1f;
            }
            else
            {
                panel.enabled = true;
                Time.timeScale = 0f;
            }
        }
    }
}
