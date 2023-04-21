using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;

    public GameObject invetoryWindow;
    public Transform dropPosition;

    [Header("Selected item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;
    public TextMeshProUGUI selectedItemStatNames;
    public TextMeshProUGUI selectedItemStatValues;
    public GameObject useButton;
    public GameObject dropButton;
    public GameObject equipButton;
    public GameObject unEquipButton;

    private int curEquipIndex;

    // components
    private PlayerController playerController;

    [Header("Events")]
    public UnityEvent onOpenInventory;
    public UnityEvent onCloseInventory;

    // singleton
    public static Inventory instance;

    private void Awake()
    {
        instance = this; 
        playerController = GetComponent<PlayerController>();
    }


}

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}