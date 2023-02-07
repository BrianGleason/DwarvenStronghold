using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePreview : MonoBehaviour
{
    // Need cooldown/gold constraints?

    public GameObject[] prefabs;
    private GameObject previewObject;

    private Camera mainCamera;

    private int selectedPrefabIndex = -1;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
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
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(prefabs[selectedPrefabIndex], previewObject.transform.position, previewObject.transform.rotation);
            Destroy(previewObject);
            selectedPrefabIndex = -1;
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

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, mainCamera.nearClipPlane));
        if (worldPos.x > -2)
        {
            worldPos = new Vector2(-2, worldPos.y);
        }
        previewObject.transform.position = worldPos;
    }
}