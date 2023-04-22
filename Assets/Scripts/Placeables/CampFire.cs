using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : Building, IInteractable
{

    public GameObject particle;
    public GameObject lightFire;
    private bool isOn;
    private Vector3 lightFireStartPos;

    [Header("Damage")]
    public int damage;
    public float damageRate;

    private List<IDamageable> thingsToDamage = new List<IDamageable>();

    void Start()
    {
        lightFireStartPos = lightFire.transform.localPosition;
        StartCoroutine(DealDamage());
    }

    IEnumerator DealDamage()
    {
        while (true)
        {
            if(isOn)
            {
                for (int i = 0; i < thingsToDamage.Count; i++)
                {
                    thingsToDamage[i].TakePhysicalDamage(damage);
                }
            }
            
            yield return new WaitForSeconds(damageRate);
        }
    }
    
    public string GetInteractPrompt()
    {
        return isOn ? "Turn Off" : "Turn On";
    }

    public void OnInteract()
    {
        isOn = !isOn;
        lightFire.SetActive(isOn);
        particle.SetActive(isOn);
    }

    void Update()
    {
        if (isOn)
        {
            float x = Mathf.PerlinNoise(Time.time * 3.0f, 0.0f) / 5.0f;
            float z = Mathf.PerlinNoise(0.0f, Time.time * 3.0f) / 5.0f;

            lightFire.transform.localPosition = lightFireStartPos + new Vector3(x, 0.0f, z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IDamageable>() != null)
        {
            thingsToDamage.Add(other.gameObject.GetComponent<IDamageable>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.GetComponent <IDamageable>() != null)
        {
            thingsToDamage.Remove(other.gameObject.GetComponent<IDamageable>());
        }
    }
}

