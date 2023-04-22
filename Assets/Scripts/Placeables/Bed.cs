using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Building, IInteractable
{

    public float wakeUpTime;
    public float startCanSleepTime;
    public float endCanSleepTime;
    private float sleepToGive;

    public string GetInteractPrompt()
    {
        return CanSleep() ? "Sleep" : "Can't sleep right now";
    }

    public void OnInteract()
    {
        if(CanSleep())
        {
            // calculate the quantity of sleep to give according to the time the player is sleeping
            float sleepToGive = Mathf.Abs(DayNightCycle.instance.time - wakeUpTime) * 1000;
            Debug.Log("Sleeping " + sleepToGive);
            DayNightCycle.instance.time = wakeUpTime;
            PlayerNeeds.instance.Sleep(sleepToGive);
        }
    }

    bool CanSleep()
    {
        return DayNightCycle.instance.time >= startCanSleepTime || DayNightCycle.instance.time <= endCanSleepTime;
    }
}
