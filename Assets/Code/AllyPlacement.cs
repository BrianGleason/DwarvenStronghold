using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyPlacement : MonoBehaviour
{
    // Need cooldown/gold constraints?

    public GameObject[] prefabs;
    private GameObject previewObject;
    public Transform placementRange;
    public SpriteRenderer range;

    private Camera mainCamera;

    private int selectedPrefabIndex = -1;
    public bool previewing;

    public float[] cooldowns = new float[3];

    private void Start()
    {
        mainCamera = Camera.main;
        range = placementRange.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if (cooldowns[i] > 0)
            {
                cooldowns[i] -= Time.deltaTime;
            }
        }

        range.color = new Color(0, 0, 0, 0);

        if (Input.GetKeyDown(KeyCode.Alpha1)) // 1
        {   if (selectedPrefabIndex == 0)
            {
                Destroy(previewObject);
                selectedPrefabIndex = -1;
                return;
            }   selectedPrefabIndex = 0;
        }   else if (Input.GetKeyDown(KeyCode.Alpha2)) // 2
        {   if (selectedPrefabIndex == 1)
            {
                Destroy(previewObject);
                selectedPrefabIndex = -1;
                return;
            }   selectedPrefabIndex = 1;
        }   else if (Input.GetKeyDown(KeyCode.Alpha3)) // 3
        {   if (selectedPrefabIndex == 2)
            {
                Destroy(previewObject);
                selectedPrefabIndex = -1;
                return;
            }   selectedPrefabIndex = 2;
        }

        if (selectedPrefabIndex == -1)
        {
            previewing = false;
            return;
        }
        else
        {
            previewing = true;
        }

        if (Input.GetMouseButtonDown(1) && selectedPrefabIndex != -1)
        {
            int allies = FindObjectsOfType<Dwarf>().Length + FindObjectsOfType<HealerDwarf>().Length + FindObjectsOfType<BerserkerDwarf>().Length;
            if (SystemControl.instance.gold >= (10 * selectedPrefabIndex + allies * 5) && cooldowns[selectedPrefabIndex] <= 0)
            {
                Instantiate(prefabs[selectedPrefabIndex], previewObject.transform.position, previewObject.transform.rotation);
                Destroy(previewObject);
                SystemControl.instance.UseGold(10 * selectedPrefabIndex + allies * 5 + 5);
                if (selectedPrefabIndex == 2)
                {
                    SystemControl.instance.UseGold(50);
                }
                cooldowns[selectedPrefabIndex] = 0.5f;
                selectedPrefabIndex = -1;
            }
            return;
        }

        else if (previewObject == null)
        {
            previewObject = Instantiate(prefabs[selectedPrefabIndex]);
            SpriteRenderer renderer = previewObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
        }

        else if (previewObject.name != prefabs[selectedPrefabIndex].name)
        {
            Destroy(previewObject);
            previewObject = Instantiate(prefabs[selectedPrefabIndex]);
            SpriteRenderer renderer = previewObject.GetComponent<SpriteRenderer>();
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
        }

        if (selectedPrefabIndex != -1)
        {
            range.color = new Color(range.color.r, range.color.g, range.color.b, 0.2f);
        }

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        if (worldPos.x > -2)
        {
            worldPos = new Vector2(-2, worldPos.y);
        }
        previewObject.transform.position = worldPos;
    }
}