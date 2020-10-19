using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ElementalAffinity
{
    [EnumFlags]
    public ElementalType baseElements;
    protected ElementalType _value;

    protected bool isDirty = true;

    [SerializeField]
    private List<ElementalModifier> elements;

    public ElementalAffinity()
    {
        elements = new List<ElementalModifier>();
    }

    public ElementalType Value
    {
        get
        {
            if (isDirty)
            {
                _value =  GetFinalValue();
                isDirty = false;
            }

            return _value;
        }
    }

    private ElementalType GetFinalValue()
    {
        ElementalType finalValue = baseElements;

        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].action == Operator.Add)
            {
                finalValue |= elements[i].targetElement;
            }
            
            if(elements[i].action == Operator.Subtract)
            {
                finalValue &= (~elements[i].targetElement);
            }
        }

        return finalValue;
    }

    public void AddModifier(ElementalModifier mod)
    {
        isDirty = true;
        elements.Add(mod);
        elements.Sort(CompareModifierOrder);
    }

    public bool RemoveModifier(ElementalModifier mod)
    {
        if (elements.Remove(mod))
        {
            isDirty = true;
            return true;
        }

        return false;
    }

    public void AddRealElementalAffinity(ElementalType mod)
    {
        baseElements |= mod;
        isDirty = true;
    }

    public void RemoveRealElementalAffinity(ElementalType mod)
    {
        baseElements &= (~mod);
        isDirty = true;
    }

    private int CompareModifierOrder(ElementalModifier x, ElementalModifier y)
    {
        if (x.Order < y.Order)
        {
            return -1;
        }

        else if (x.Order > y.Order)
        {
            return 1;
        }

        return 0;
    }

}

[System.Serializable]
public class ElementalModifier
{
    public ElementalType targetElement;
    public Operator action;
    public int Order { get { return (int)action; } }
}

public enum Operator
{
    Add,
    Subtract
}