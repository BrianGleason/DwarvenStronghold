using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    private float fadeTime = 0.5f;
    public bool gameOver;
    public bool win;
    private SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameOver = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            StartCoroutine(FadeToRed());
        }
        if (win)
        {
            StartCoroutine(FadeToWhite());
        }
    }

    private IEnumerator FadeToRed()
    {
        Color originalColor = spriteRenderer.color;
        Color targetColor = Color.red;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeTime);
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Game Over");
        yield return null;
    }

    private IEnumerator FadeToWhite()
    {
        Color originalColor = spriteRenderer.color;
        Color targetColor = Color.white;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeTime);
            spriteRenderer.color = Color.Lerp(originalColor, targetColor, t);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("Victory");
        yield return null;
    }
}
