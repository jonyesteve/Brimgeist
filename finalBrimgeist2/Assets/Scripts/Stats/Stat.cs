using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using static Types;
using System.Drawing.Printing;
using System;

[System.Serializable]
public class Stat
{
    public float baseValue;
    private readonly List<StatModifier> ValueModifiers = new();
    public readonly ReadOnlyCollection<StatModifier> valueModifiers;
    public string StatName { get; set; }

    public float lastFinalValue = float.MinValue;
    public float FinalValue
    {
        get
        {
            if (recentlyChanged || baseValue != lastFinalValue)
            {
                lastFinalValue = baseValue;
                finalValue = GetStatValue();
                recentlyChanged = false;
            }
            return (float)finalValue;
            
        }
    }

    [SerializeField]private float finalValue;

    [SerializeField] private bool recentlyChanged = true;


    public Stat(int basevalue, string statName)
    {
        this.baseValue = basevalue;
        StatName = statName;
        lastFinalValue = float.MinValue;
        ValueModifiers = new List<StatModifier>();
        valueModifiers = ValueModifiers.AsReadOnly();
    }



    public void RemoveAllModifiersFromSource(object source)
    {
        for (int i = ValueModifiers.Count - 1; i >= 0; i--)
        {
            if(ValueModifiers[i].source == source)
            {
                recentlyChanged = true;
                ValueModifiers.RemoveAt(i);
            }
        }
    }


    public void AddModifier(StatModifier statModifier)
    {
        recentlyChanged = true;
        ValueModifiers.Add(statModifier);
        ValueModifiers.Sort(CompareModifierOrder);
    }
    public void RemoveModifier(StatModifier statModifier)
    {
        if (ValueModifiers.Remove(statModifier)) recentlyChanged = true;
    }

    public float GetStatValue()
    {
        finalValue = baseValue;
        float sumPercentAdd = 0;
        for(int i = 0; i < ValueModifiers?.Count; i++)
        {
            if(ValueModifiers[i].type == StatModType.Flat)
            {
                finalValue += ValueModifiers[i].value;
            }
            else if(ValueModifiers[i].type == StatModType.PercentAdd)
            {
                finalValue *= 1 + ValueModifiers[i].value;
                if(i + 1 >= ValueModifiers.Count || ValueModifiers[i+1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (ValueModifiers[i].type == StatModType.PercentMult)
            {
                finalValue *= (100 + ValueModifiers[i].value)/100;
            }
        }
        return finalValue;
    }
    int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
            return -1;
        else if (a.order > b.order)
            return 1;
        return 0;
    }

    
    public static implicit operator float(Stat s) => s.FinalValue;
    public static implicit operator int(Stat s) => (int)s.FinalValue;
}

