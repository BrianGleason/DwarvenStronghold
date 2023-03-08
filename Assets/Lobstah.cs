using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobstah : MonoBehaviour
{
    public GameObject talkPrefab;
    public GameObject talkText;
    private ShadowFade start;
    private bool started;

    private string[] insults = {"Don't worry, I won't tell anyone you had to\nstand on a box to fight me.",
                                "I could fit two of you in my claws...\nmaybe even three!",
                                "I heard you can fit in a teacup.\nCan you confirm?",
                                "You're so small, I almost\nmistook you for a hermit crab!",
                                "You know, I never realized how small you are\nuntil I imagined you next to a shrimp.",
                                "How's the weather down there?\nI wouldn't know, I'm not that tiny...",
                                "Can you even climb up my claw?\nMaybe bring a stool next time."};

    private List<int> usedIndices = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        start = FindObjectOfType<ShadowFade>();
        talkText = Instantiate(talkPrefab, this.transform.position + Vector3.up * 2 + Vector3.left * 2, Quaternion.identity, this.transform);
        talkText.transform.localScale = new Vector3(0.3f, 0.3f, 1);

        started = false;
    }

    // Update is called once per frame
    void Update()
    {
        TextMesh txt = talkText.transform.GetComponent<TextMesh>();

        if (!start.revealed)
        {
            txt.text = "";
        }

        if (start.revealed && !started)
        {
            started = true;
            Cycle();
        }
    }

    void Cycle()
    {
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, insults.Length);
        }
        while (usedIndices.Contains(randomIndex));

        string newText = insults[randomIndex];
        TextMesh txt = talkText.transform.GetComponent<TextMesh>();
        txt.text = newText;

        usedIndices.Add(randomIndex);

        if (usedIndices.Count == insults.Length)
        {
            usedIndices.Clear();
        }

        Invoke("Cycle", 3f);
    }
}
