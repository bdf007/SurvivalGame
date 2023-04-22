using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    public override void OnAltAttackInput()
    {
        
    }

    public void SetNewBuildingRecipe(BuildingRecipe recipe)
    {

    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
