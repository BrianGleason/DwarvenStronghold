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
        gold = 30;
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
