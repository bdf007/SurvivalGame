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
    public bool doesDealDamage;
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
    
    // called by animation event
    public void Onhit()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, attackDistance))
        {
            // if the player is hitting a ressource
            if (doesGatherRessources && hit.collider.GetComponent<Ressource>())
            {
                hit.collider.GetComponent<Ressource>().Gather(hit.point, hit.normal);
            }
            // if the player is hitting an enemy
            if (doesDealDamage && hit.collider.GetComponent<IDamageable>() != null)
            {
                hit.collider.GetComponent<IDamageable>().TakePhysicalDamage(damage);
            }
        }
    }
}
