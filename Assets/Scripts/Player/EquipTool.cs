using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{

    public float attackRate;
    private bool isAttacking;
    public float attackDistance;


    [Header("Ressource Gathering")]
    public bool doesGatherRessources;

    [Header("Combat")]
    public bool doesDamage;
    public int damage;

    // components
    private Animator animator;
    private Camera cam;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        cam = Camera.main;
    }

    public override void OnAttackInput()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            Invoke("OnAttack", attackRate);
        }
    }

    void OnAttack()
    {
        isAttacking = false;
    }
    
    public void Onhit()
    {
        Debug.Log("hit detected");
    }
}
