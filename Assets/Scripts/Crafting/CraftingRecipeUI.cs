using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipeUI : MonoBehaviour
{
    public CraftingRecipe recipe;
    public Image backgroundImage;
    public Image icon;
    public TextMeshProUGUI itemName;
    public Image[] ressourceCosts;

    public Color canCraftColor;
    public Color cannotCraftColor;
    private bool canCraft;

    private void OnEnable()
    {
        UpdateCanCraft();
    }

    public void UpdateCanCraft()
    {
        canCraft = true;
        for (int i = 0; i < recipe.costs.Length; i++)
        {
            if (!Inventory.instance.HasItems(recipe.costs[i].item, recipe.costs[i].quantity))
            {
                canCraft = false;
                break;
            }
        }

        backgroundImage.color = canCraft ? canCraftColor : cannotCraftColor;
    }

    private void Start()
    {
        icon.sprite = recipe.itemToCraft.icon;
        itemName.text = recipe.itemToCraft.displayName;
        for (int i = 0; i < ressourceCosts.Length; i++)
        {
            if (i < recipe.costs.Length)
            {
                ressourceCosts[i].gameObject.SetActive(true);
                ressourceCosts[i].sprite = recipe.costs[i].item.icon;
                ressourceCosts[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = recipe.costs[i].quantity.ToString();
            }
            else
            {
                ressourceCosts[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnClickButton()
    {
        if(canCraft)
        {
            CraftingWindow.instance.Craft(recipe);
        }
    }
}
