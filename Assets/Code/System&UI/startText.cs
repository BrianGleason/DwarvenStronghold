using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startText : MonoBehaviour
{
    public TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = Mathf.PingPong(Time.time, 1);
        Color color = textMesh.color;
        color.a = alpha;
        textMesh.color = color;

        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Jason Demo");
        }
    }
}
