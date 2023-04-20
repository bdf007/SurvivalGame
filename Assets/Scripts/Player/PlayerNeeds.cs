using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;

    public UnityEvent onTakeDamage;

    private void Start()
    {
        // set the starting values
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        thirst.curValue = thirst.startValue;
        sleep.curValue = sleep.startValue;
    }

    private void Update()
    {
        // decay needs over time
        hunger.Remove(hunger.decayRate * Time.deltaTime);
        thirst.Remove(thirst.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);

        // decay health if needs are low
        if (hunger.curValue == 0.0f)
        {
            health.Remove(noHungerHealthDecay * Time.deltaTime);
        }
        if (thirst.curValue == 0.0f)
        {
            health.Remove(noThirstHealthDecay * Time.deltaTime);
        }

        // check if the player is dead
        if (health.curValue == 0.0f)
        {
            Die();
        }

        // Update UI bars
        health.uiBar.fillAmount = health.GetPercent();
        hunger.uiBar.fillAmount = hunger.GetPercent();
        thirst.uiBar.fillAmount = thirst.GetPercent();
        sleep.uiBar.fillAmount = sleep.GetPercent();
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat (float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    public void Sleep(float amount)
    {
        sleep.Remove(amount);
    }

    public void TakePhysicalDamage(float amount)
    {
        health.Remove(amount);
        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Player died");
    }
}

[System.Serializable]
public class Need
{
    [HideInInspector] public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    // add to the need
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // remove from the need
    public void Remove(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0);
    }

    // get the current value as a percentage
    public float GetPercent()
    {
        return curValue / maxValue;
    }
}
