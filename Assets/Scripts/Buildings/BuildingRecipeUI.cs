using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingRecipeUI : MonoBehaviour
{
    public BuildingRecipe recipe;
    public Image backgroundImage;
    public Image icon;
    public TextMeshProUGUI buildingName;
    public Image[] ressourceCosts;

    public Color canBuildColor;
    public Color cantBuildColor;
    private bool canBuild;

    private void OnEnable()
    {
        UpdateCanCraft();
    }

    private void Start()
    {
        icon.sprite = recipe.icon;
        buildingName.text = recipe.displayName;

        for (int i = 0; i < ressourceCosts.Length; i++)
        {
            if (i < recipe.cost.Length)
            {
                ressourceCosts[i].gameObject.SetActive(true);

                ressourceCosts[i].sprite = recipe.cost[i].item.icon;
                ressourceCosts[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = recipe.cost[i].quantity.ToString();
            }
            else
            {
                ressourceCosts[i].gameObject.SetActive(false);
            }
        }
    }

    void UpdateCanCraft()
    {
        canBuild = true;

        for (int i = 0; i < recipe.cost.Length; i++)
        {
            if (!Inventory.instance.HasItems(recipe.cost[i].item, recipe.cost[i].quantity))
            {
                canBuild = false;
                break;
            }
        }
        
        backgroundImage.color = canBuild ? canBuildColor : cantBuildColor;
    }

    public void OnClickButton()
    {
        if(canBuild)
        {
            EquipBuildingKit.instance.SetNewBuildingRecipe(recipe);
        }
        else
        {
            PlayerController.instance.ToggleCursor(false);
            EquipBuildingKit.instance.buildingWindow.SetActive(false);
        }
    }
        
 }
