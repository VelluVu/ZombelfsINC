
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[Serializable]
public class Stat
{

    [SerializeField]
    private float baseValue;

    public virtual float value
    {
        get
        {
            if (isDirty || baseValue != lastBaseValue)
            {
                lastBaseValue = baseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> statModifiers;
    protected readonly ReadOnlyCollection<StatModifier> _statModifiers;

    public Stat()
    {
        statModifiers = new List<StatModifier>();
        _statModifiers = statModifiers.AsReadOnly();
    }

    public Stat(float baseValue) : this()
    {
        this.baseValue = baseValue;
       
    }

    public virtual void AddModifier(StatModifier modifier)
    {
        isDirty = true;
        statModifiers.Add(modifier);
        statModifiers.Sort(CompareModifierOrder);

    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.order < b.order)
        {
            return -1;
        } else if (a.order > b.order)
        {
            return 1;
        }
        return 0;
    }

    public virtual bool RemoveModifier(StatModifier modifier)
    {

        
       if (statModifiers.Remove(modifier))
        {
            isDirty = true;
            return true;
        }
        return false;

    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {

        bool didRemove = false;

        for (int i = statModifiers.Count; i >= 0; i--)
        {
            if(statModifiers[i].source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = baseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.type == StatModType.Flat)
            {
                finalValue += statModifiers[i].value;
            }
            else if(mod.type == StatModType.PercentAdd)
            {
                sumPercentAdd += mod.value;
                if (i + 1 >= statModifiers.Count || statModifiers[i+1].type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }
}