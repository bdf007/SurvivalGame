using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Building, IInteractable
{

    public GameObject particle;
    public GameObject lightFire;
    private bool isOn;

    [Header("Damage")]
    public int damage;
    public float damageRate;

    private List<IInteractable> thingsToDamage = new List<IInteractable>();
    public string GetInteractPrompt()
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract()
    {
        throw new System.NotImplementedException();
    }
}

