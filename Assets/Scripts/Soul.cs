using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public enum SlayType
{
    Human,
    Demon,
    Mixed
}

[Serializable]
public struct SoulTracker
{
    public SlayType type;
    public float watchedValue;
    public int killsToActivate;
    public UnityEvent onCrossOver;
    public UnityEvent onCrossBack;
}

public class Soul : MonoBehaviour
{
    private static Soul Singleton;
    public static Soul singleton
    {
        get
        {
            if (!Singleton)
            {
                Singleton = GameObject.FindGameObjectWithTag("Player").GetComponent<Soul>();
            }
            return Singleton;
        }
        set
        {
            Singleton = value;
        }
    }

    public int numHumans = 0;
    public int numDemons = 0;

    public int numKilled
    {
        get { return numHumans + numDemons; }
    }

    public int startExpandingAt;
    public int stopExpandingAt;
    public int startTrackingAt;

    public List<SoulTracker> trackedValues;
    private List<SoulTracker> heldValues = new List<SoulTracker>();

    public float percentDemonSlayer = 0;
    public float percentHumanSlayer = 0;
    public float percentMixedSlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
       for (int i = trackedValues.Count - 1; i >= 0; i--)
        {
            if (trackedValues[i].killsToActivate > 0)
            {
                heldValues.Add(trackedValues[i]);
                trackedValues.RemoveAt(i);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        RegenerateValues();
    }

    private void RegenerateValues()
    {
        int mixed = Math.Min(numHumans, numDemons);
        int soloDemon = numDemons - mixed;
        int soloHuman = numHumans - mixed;
        int total = (mixed + soloDemon + soloHuman);

        float newMixedPercent = percentMixedSlayer;
        float newHumanPercent = percentHumanSlayer;
        float newDemonPercent = percentDemonSlayer;

        if (total > 0)
        {
            newMixedPercent = ((float) mixed) / total;
            newHumanPercent = ((float) soloHuman) / total;
            newDemonPercent = ((float) soloDemon) / total;
        }

        if (numKilled >= startTrackingAt)
        {
            foreach (SoulTracker tracked in trackedValues)
            {
                float oldVal = 0;
                float newVal = 0;
                switch (tracked.type)
                {
                    case SlayType.Human:
                        oldVal = percentHumanSlayer;
                        newVal = newHumanPercent;
                        break;
                    case SlayType.Demon:
                        oldVal = percentDemonSlayer;
                        newVal = newDemonPercent;
                        break;
                    case SlayType.Mixed:
                        oldVal = percentMixedSlayer;
                        newVal = newMixedPercent;
                        break;
                    default:
                        Debug.LogError($"Switch called with value of unknown type {tracked.type}", this);
                        break;
                }

                //Determine if we crossed over
                if (tracked.watchedValue >= oldVal && tracked.watchedValue < newVal)
                {
                    tracked.onCrossOver.Invoke();
                }
                else if (tracked.watchedValue >= newVal && tracked.watchedValue < oldVal)
                {
                    tracked.onCrossBack.Invoke();
                }
            }

            for (int i = heldValues.Count - 1; i >= 0; i--)
            {
                if (numKilled > heldValues[i].killsToActivate)
                {
                    //We should add it to the list
                    trackedValues.Add(heldValues[i]);

                    //Check if it should fire
                    float newVal = 0;
                    switch (heldValues[i].type)
                    {
                        case SlayType.Human:
                            newVal = newHumanPercent;
                            break;
                        case SlayType.Demon:
                            newVal = newDemonPercent;
                            break;
                        case SlayType.Mixed:
                            newVal = newMixedPercent;
                            break;
                    }
                    if (newVal > heldValues[i].watchedValue)
                    {
                        heldValues[i].onCrossOver.Invoke();
                    }

                    //Check if it should be removed
                    heldValues.RemoveAt(i);
                }
            }



            percentDemonSlayer = newDemonPercent;
            percentHumanSlayer = newHumanPercent;
            percentMixedSlayer = newMixedPercent;
            

        }

    }

    public void printSomething()
    {
        print("This fired!");
    }
}
