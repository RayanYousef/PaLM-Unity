using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCharacter : BaseCharacter
{

    [SerializeField] CharacterMods characterMods;
    public CharacterMods CharacterMods => characterMods;

    public override void OnEnable()
    {
        base.OnEnable();

        characterMods.OnAnyModChange += UpdateModStats;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        characterMods.OnAnyModChange -= UpdateModStats;
    }

    private void UpdateModStats(SO_InventoryItem item)
    {
        CharacterStats.UpdateModStats(characterMods.ReturnStats());
    }
}
