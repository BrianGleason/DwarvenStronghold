using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowFade : MonoBehaviour
{
    public MainCharacterController player;
    public SpriteRenderer sprite;

    public bool revealed;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MainCharacterController>();
        sprite = GetComponent<SpriteRenderer>();
        revealed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y <= -13)
        {
            StartCoroutine(FadeAway());
            Debug.Log("yay");
        }
    }

    IEnumerator FadeAway()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;

        Color originalColor = sprite.color;
        Color targetColor = new Color(0, 0, 0, 0);
        float elapsedTime = 0f;

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / 0.5f);
            sprite.color = Color.Lerp(originalColor, targetColor, t);
            yield return new WaitForSeconds(0.01f);
        }
        revealed = true;
        yield return null;
    }
}

