using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemControl : MonoBehaviour
{
    public int gold;
    public static SystemControl instance;

    // Start is called before the first frame update
    void Start()
    {
        gold = 0;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // Pause/Resume
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f) //continue
            {
                Time.timeScale = 1f;
            }
            else //pause
            {
                Time.timeScale = 0f;

            }
        }

        //Enemy spawning


        //Ally spawning
    }

    public void AddGold(int gain)
    {
        gold += gain;
    }

    public void UseGold(int loss)
    {
        gold -= loss;
    }
}
