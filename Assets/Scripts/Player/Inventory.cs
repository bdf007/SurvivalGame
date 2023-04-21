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

    // adds the requested item to the player's inventory
    public void AddItem(ItemData item)
    {
        // does this item have a stack it can be added to?
        if (item.canStack)
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

        // do we have an empty slot for the item?
        if (emptySlot != null)
        {
            emptySlot.item = item;
            emptySlot.quantity = 1;
            UpdateUI();
            return;
        }
        // if the item can't stack and there are no empty slots - throw it away
        TrowItem(item);
    }

    // spawns the item infront of the player
    void TrowItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360.0f));
    }

    // updates the UI slots
    void UpdateUI()
    {
        for(int i = 0; i < uiSlots.Length; i++)
        {
            if (slots[i].item != null)
            {
                uiSlots[i].Set(slots[i]);
            }
            else
            {
                uiSlots[i].Clear();
            }
        }
    }

    // returns the item slot that the requested item can be stacked on
    // returns null if there is no stack available
    ItemSlot GetItemStack(ItemData item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == item && slots[i].quantity < item.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    // returns an empty slot in the inventory
    // if there are no empty slots - return null
    ItemSlot GetEmptySlot()
    {
        for(int i =0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
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