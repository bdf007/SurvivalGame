using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit = 1;
    public int capacity;
    public GameObject hitParticle;

    // called when the player hits the resource with an axe
    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        // give the player "quantityPerHit" of the resource
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0)
            {
                Destroy(gameObject);
                break;
            }
            capacity -=1;

            Inventory.instance.AddItem(itemToGive);
        }

        Destroy(Instantiate(hitParticle, hitPoint, Quaternion.LookRotation(hitNormal, Vector3.up)), 1.0f);

        if (capacity <= 0)
        {
            Destroy(gameObject);
        }


    }
}
