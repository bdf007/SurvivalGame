using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamageable> thingsToDamage = new List<IDamageable>();

    private void Start()
    {
        StartCoroutine(DealDamage());
    }
    IEnumerator DealDamage()
    {
        while(true)
        {
            for(int i = 0; i < thingsToDamage.Count; i++)
            {
                thingsToDamage[i].TakePhysicalDamage(damage);
            }
            yield return new WaitForSeconds(damageRate);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       // if it's an IDamageable, add it to the list
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            thingsToDamage.Add(collision.gameObject.GetComponent<IDamageable>());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // if it's an IDamageable, remove it from the list
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            thingsToDamage.Remove(collision.gameObject.GetComponent<IDamageable>());
        }
    }
}
