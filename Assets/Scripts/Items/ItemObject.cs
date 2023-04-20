using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData ItemData;
    public string GetInteractPrompt()
    {
       return string.Format("Pickup {0}", ItemData.displayName);
    }
    public void OnInteract()
    {
        Destroy(gameObject);
    }

}
