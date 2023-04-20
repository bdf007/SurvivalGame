using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamageable> thingsToDamage = new List<IDamageable>();

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
}
