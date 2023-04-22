using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipBuildingKit : Equip
{
    public GameObject buildingWindow;
    private BuildingRecipe curRecipe;
    private BuildingPreview curBuildingPreview;

    public float placementUpdateRate = 0.03f;
    private float lastPlacementUpdateTime;
    public float placementMaxDistance = 5.0f;

    public LayerMask placementLayerMask;

    public Vector3 placementPosition;
    private bool canPlace;
    private float curYrot;

    public float rotationSpeed = 180.0f;

    private Camera cam;
    public static EquipBuildingKit instance;

    private void Awake()
    {
        instance = this;
        cam = Camera.main;
    }

    private void Start()
    {
        buildingWindow = FindObjectOfType<BuildingWindow>(true).gameObject;
    }

    public override void OnAttackInput()
    {
        Debug.Log("OnAttackInput");
        if(curRecipe == null || curBuildingPreview == null || !canPlace)
        {
            Debug.Log("OnAttackInput - return");
            return;
        }

        Instantiate(curRecipe.spawnPrefab, curBuildingPreview.transform.position, curBuildingPreview.transform.rotation);

        for(int i = 0; i < curRecipe.cost.Length; i++)
        {
            for (int j= 0; j < curRecipe.cost[i].quantity; j++)
            {
                Inventory.instance.RemoveItem(curRecipe.cost[i].item);
            }
        }

        curRecipe = null;
        Destroy(curBuildingPreview.gameObject);
        curBuildingPreview = null;
        canPlace = false;
        curYrot = 0;
    }

    public override void OnAltAttackInput()
    {
        buildingWindow.SetActive(true);
        PlayerController.instance.ToggleCursor(true);
    }

    public void SetNewBuildingRecipe(BuildingRecipe recipe)
    {
        curRecipe = recipe;
        buildingWindow.SetActive(false);
        PlayerController.instance.ToggleCursor(false);

        curBuildingPreview = Instantiate(recipe.previewPrefab).GetComponent<BuildingPreview>();
    }

    private void Update()
    {
        // do we have a recipe selected?
        if (curRecipe != null && curBuildingPreview != null && Time.time - lastPlacementUpdateTime > placementUpdateRate)
        {
            lastPlacementUpdateTime = Time.time;
            // shoot a raycast to where we're looking
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, placementMaxDistance, placementLayerMask))
            {
                curBuildingPreview.transform.position = hit.point;
                curBuildingPreview.transform.up = hit.normal;
                curBuildingPreview.transform.Rotate(new Vector3(0.0f, curYrot, 0.0f), Space.Self);

                if(!curBuildingPreview.CollidingWithObjects())
                {
                    if(!canPlace)
                    {
                        curBuildingPreview.CanPlace();
                    }
                    canPlace = true;
                }
                else
                {
                    if(canPlace)
                    {
                        curBuildingPreview.CantPlace();
                    }
                    canPlace = false;
                }

            }
        }

        if (Keyboard.current.rKey.isPressed)
        {
            curYrot += rotationSpeed * Time.deltaTime;
            if(curYrot > 360.0f)
            {
                curYrot = 0.0f;
            }
        }
    }

    private void OnDestroy()
    {
        
    }
}
