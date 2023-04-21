using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{

    public CraftingWindow craftingWindow;
    private PlayerController playerController;

    void Start()
    {
    craftingWindow = FindObjectOfType<CraftingWindow>(true);
    playerController = FindObjectOfType<PlayerController>();
    }
    public string GetInteractPrompt()
    {
        return "Craft";
    }

    public void OnInteract()
    {
        craftingWindow.gameObject.SetActive(true);
        playerController.ToggleCursor(true);
    }
}
