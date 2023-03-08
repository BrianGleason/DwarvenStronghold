using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseText : MonoBehaviour
{
    private TextMesh Text;

    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TextMesh>();
        Text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Text.text = "";
            }
            else
            {
                Text.text = "The game is paused. Press esc to continue.\n" +
                    "Press R to return to home screen.";
                Text.fontSize = 20;
            }
        }
        var mainCharController = FindObjectOfType<MainCharacterController>();
        if (mainCharController)
        {
            if (mainCharController.transform.position.y < -5)
            {
                Text.transform.position = new Vector3(0, -11, 0);
            }
            else
            {
                Text.transform.position = new Vector3(0, 0, 0);
            }
        }
        else
        {
            Text.transform.position = new Vector3(0, 0, 0);
        }
    }
}
