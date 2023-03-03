using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeText : MonoBehaviour
{
    public TextMesh text;
    public MainCharacterController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MainCharacterController>();
        text = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Abs(player.transform.position.y - transform.position.y);
        float alpha = Mathf.Lerp(0f, 1f, 1f - Mathf.Clamp01(distance / 0.5f));
        Color color = new Color(1f, 1f, 1f, alpha);
        text.color = color;
    }
}
