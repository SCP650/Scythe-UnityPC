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
    public int watchedValue;
    public UnityEvent onCrossOver;
    public UnityEvent onCrossBack;
}

public class Soul : MonoBehaviour
{
    public int numHumans = 0;
    public int numDemons = 0;

    public List<SoulTracker> trackedValues;

    public int percentDemonSlayer = 0;
    public int percentHumanSlayer = 0;
    public int percentMixedSlayer = 100;

    // Start is called before the first frame update
    void Start()
    {
        
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

        int newMixedPercent = percentMixedSlayer;
        int newHumanPercent = percentHumanSlayer;
        int newDemonPercent = percentDemonSlayer;

        if (total > 0)
        {
            newMixedPercent = (100 * mixed) / total;
            newHumanPercent = (100 * soloHuman) / total;
            newDemonPercent = (100 * soloDemon) / total;
        }

        foreach (SoulTracker tracked in trackedValues)
        {
            int oldVal = 0;
            int newVal = 0;
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

        percentDemonSlayer = newDemonPercent;
        percentHumanSlayer = newHumanPercent;
        percentMixedSlayer = newMixedPercent;
    }
}
