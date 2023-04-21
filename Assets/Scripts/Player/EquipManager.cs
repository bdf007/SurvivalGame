using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipManager : MonoBehaviour
{
    // to test equip
    //public ItemData testEquip;

    public Equip curEquip;
    public Transform equipParent;

    private PlayerController playerController;

    // singleton
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
        playerController = GetComponent<PlayerController>();
    }

    //private void Start()
    //{
    //    To test equip
    //    EquipNew(testEquip);
    //}

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && playerController.canLook == true)
        {
            
        }
    }

    public void OnAltAttackInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && curEquip != null && playerController.canLook == true)
        {

        }
    }

    public void EquipNew(ItemData item)
    {
        UnEquip();
        curEquip = Instantiate(item.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if(curEquip != null)
        {
            Destroy(curEquip.gameObject);
            curEquip = null;
        }
    }
}
