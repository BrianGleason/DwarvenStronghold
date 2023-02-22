using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerRespawner : MonoBehaviour
{
    GameObject player;
    bool respawning;
    public float respawncooldown = 2.50f;
    public GameObject PlayerPrefab;
    private Vector3 pos;
    private MainCharacterController cont;
    public Camera cam;
    //public SpriteRenderer healthbar;


    // Start is called before the first frame update
    void Start()
    {
        respawning = false;
        pos = transform.position;
        pos.x += 2.0f;
        player = GameObject.FindWithTag("Player");
        cont = player.GetComponent<MainCharacterController>();
        cam = cont.cam;
        //healthbar = cont.healthbar;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        if (player == null && !respawning)
        {
            respawning = true;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(respawncooldown);
        player = Instantiate(PlayerPrefab, pos, Quaternion.identity);
        cont = player.GetComponent<MainCharacterController>();
        cont.cam = cam;
        //cont.healthbar = healthbar;
        respawning = false;
    }
}
