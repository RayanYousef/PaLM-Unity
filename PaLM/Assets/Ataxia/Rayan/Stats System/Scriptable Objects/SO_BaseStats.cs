using Stats_System;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats Classes/Base Stats")]
public class SO_BaseStats : ScriptableObject
{
    [SerializeField] private GenericStats genStats;


    [SerializeField] private CharacterStats stats;

    public CharacterStats Stats => stats;
}
