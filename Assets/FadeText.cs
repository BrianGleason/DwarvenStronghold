using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    public TextMesh text;
    public MainCharacterController player;
    public ShadowFade shadow;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MainCharacterController>();
        text = GetComponent<TextMesh>();
        shadow = FindObjectOfType<ShadowFade>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(player.transform.position.y - transform.position.y);
        float alpha = Mathf.Lerp(0f, 1f, 1f - Mathf.Clamp01(distance / 1f));
        Color color = new Color(1f, 1f, 1f, alpha);
        text.color = color;

        if (shadow.revealed)
        {
            text.text = "";
        }
    }
}
