using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
    public CraftingRecipeUI[] recipeUIs;

    // singleton
    public static CraftingWindow instance;

    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        Inventory.instance.onOpenInventory.AddListener(OnOpenInventory);
    }

    void OnDisable()
    {
        Inventory.instance.onOpenInventory.RemoveListener(OnOpenInventory);
    }

    void OnOpenInventory()
    {
        gameObject.SetActive(false);
    }

    public void Craft(CraftingRecipe recipe)
    {
        // remove the items it costs to craft from our inventory
        for( int i = 0; i < recipe.costs.Length; i++)
        {
            for (int j = 0; j < recipe.costs[i].quantity; j++)
            {
                Inventory.instance.RemoveItem(recipe.costs[i].item);
            }
        }

        // add the crafted item to our inventory
        Inventory.instance.AddItem(recipe.itemToCraft);

        // update the crafting recipe UIs
        for (int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].UpdateCanCraft();
        }
    }
}
