using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNeeds : MonoBehaviour
{
    public Need health;
    public Need hunger;
    public Need thirst;
    public Need sleep;

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;

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
    public void AddValue(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // remove from the need
    public void RemoveValue(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0);
    }

    // get the current value as a percentage
    public float GetPercent()
    {
        return curValue / maxValue;
    }
}
