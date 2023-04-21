using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting Recipe", menuName = " New Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    public ItemData itemToCraft;
    public RessourceCost[] costs;

}

[System.Serializable]
public class RessourceCost
{
    public ItemData item;
    public int quantity;
}
