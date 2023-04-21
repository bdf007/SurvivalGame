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

    private void Start()
    {
        invetoryWindow.SetActive(false);
        slots = new ItemSlot[uiSlots.Length];

        // initialize the slots
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = new ItemSlot();
            uiSlots[i].index = i;
            uiSlots[i].Clear();
        }

    }

    public void Toggle()
    {

    }

    public bool IsOpen()
    {
        return invetoryWindow.activeInHierarchy;
    }

    public void AddItem(ItemData item)
    {
        if(item.canStack)
        {
            ItemSlot slotToStackTo = GetItemStack(item);
            if(slotToStackTo != null)
            {
                slotToStackTo.quantity++;
                UpdateUI();
                return;
            }
        }

        ItemSlot emptySlot = GetEmptySlot();

        if(emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }

        TrowItem(item);
    }

    void TrowItem(ItemData item)
    {

    }

    void UpdateUI()
    {

    }

    ItemSlot GetItemStack(ItemData item)
    {
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        return null;
    }

    public void SelectecItem()
    {

    }

    public void ClearSelectedItemWindow()
    {

    }

    public void OnUseButton()
    {

    }

    public void OnDropButton()
    {

    }

    public void OnEquipButton()
    {

    }

    void UnEquip(int index)
    {

    }

    public void OnUnEquipButton ()
    {

    }

    void RemoveSelectedItem ()
    {

    }

    public void RemoveItem (ItemData item)
    {

    }

    public bool HasItems (ItemData item, int quantity)
    {
        return false;
    }

}

public class ItemSlot
{
    public ItemData item;
    public int quantity;
}