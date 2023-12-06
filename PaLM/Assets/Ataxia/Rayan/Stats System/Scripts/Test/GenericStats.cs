
using System;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;


public enum StatType
{
    lvl,
    xp,
    movement,
    teamSlots,
    maxHP,
    meleeAtk,
    meleeAcc,
    rangedAtk,
    rangedAcc,
    meleeDef,
    rangedDef,
    critChance,
    spd,
    hackStr,
    hackChance,
    cyberSec
}

[Serializable]
public class GenericStats
{
    DynamicStructVariableSetterGetter setterGetter = new DynamicStructVariableSetterGetter();

    [Header("General Stats")]
    [SerializeField] private Stat<int> movement;
    [SerializeField] private Stat<int> teamSlots;
    private Stat<int> lvl;
    private Stat<int> xp;

    // Combat Stats
    [Header("Combat Stats")]
    [SerializeField] private Stat<float> maxHP;
    [SerializeField] private Stat<float> meleeAtk;
    [SerializeField] private Stat<float> meleeAcc;
    [SerializeField] private Stat<float> rangedAtk;
    [SerializeField] private Stat<float> rangedAcc;
    [SerializeField] private Stat<float> meleeDef;
    [SerializeField] private Stat<float> rangedDef;
    [SerializeField] private Stat<float> critChance;
    [SerializeField] private Stat<float> spd;
    [SerializeField] private Stat<float> hackStr;
    [SerializeField] private Stat<float> hackChance;
    [SerializeField] private Stat<float> cyberSec;

    public Stat<T> GetStatOfType<T>(StatType type) where T : IConvertible, IFormattable, IComparable
    {
        FieldInfo field = GetType().GetField(type.ToString(), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

        if (field != null)
        {
            return GetValueAsStat<T>(field);
        }
        else
        {
            Debug.LogError($"Field {type} not found.");
        }

        return null;
    }

    private Stat<T> GetValueAsStat<T>(FieldInfo field) where T : IConvertible, IFormattable, IComparable
    {
        if (field != null)
        {
            // Ensure that private fields can be accessed
            if (!field.IsPublic)
            {
                field = GetType().GetField(field.Name, BindingFlags.Instance | BindingFlags.NonPublic);
            }

            var value = field.GetValue(this);

            if (value is Stat<T> stat)
            {
                return stat;
            }
            else
            {
                Debug.LogError($"Field is not of type Stat<{typeof(T)}>.");
                return null;
            }
        }
        else
        {
            Debug.LogError("Field is null.");
            return null;
        }
    }

}
