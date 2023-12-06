using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat<T>  where T : IConvertible,IFormattable, IComparable
{
    public event Action<Stat<T>> OnStatUpdated;

    [SerializeField] private T baseValue;
    [SerializeField] private T totalValue;

    [SerializeField] private Dictionary<string, T> statModifiers = new Dictionary<string, T>();

    public T BaseValue => baseValue;
    public T TotalValue => totalValue;

    private void UpdateStat()
    {
        totalValue = baseValue;
        foreach (var modifier in statModifiers)
        {
            totalValue = (T)Convert.ChangeType(totalValue.ToDouble(null) + modifier.Value.ToDouble(null), typeof(T));
        }
        OnStatUpdated?.Invoke(this);
    }

    public  void AddModifier(string modifierSourceKey, T newModifier)
    {
        statModifiers.Add(modifierSourceKey, newModifier);
        UpdateStat();
        Debug.Log(modifierSourceKey + ": " + newModifier + ", total value:" + totalValue);

    }
    public  void RemoveModifier(string modifierKey)
    {
        statModifiers.Remove(modifierKey);
        UpdateStat();
    }
    public  void ClearStatModifiers()
    {
        statModifiers.Clear();
        UpdateStat();
    }
}
