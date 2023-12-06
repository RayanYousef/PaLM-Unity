using System;
using System.Collections.Generic;
using UnityEngine;

public enum EquipSlot { WEAPON_MAIN, WEAPON_SECONDARY, ARMOR, CYBERNETICS, CONSUMABLE_1, CONSUMABLE_2, SPECIAL_MOD_SLOT }
public class InventorySystem : MonoBehaviour
{

    //public static InventorySystem Current;
    public List<SO_InventoryItem> Items { get { return OwnedItems; } }
    public float RealKrones { get { return realKrones; } set { realKrones = Mathf.Clamp(value, 0, float.MaxValue); } }

    public void EquipTeamMemberItem(int team_mate_index, SO_InventoryItem item, EquipSlot slot)
    {
        UnEquipTeamMemberItem(team_mate_index, item, slot);
        switch (slot)
        {
            case EquipSlot.WEAPON_MAIN:
                TeamMates[team_mate_index].MainWeapon = item;
                break;
            case EquipSlot.WEAPON_SECONDARY:
                TeamMates[team_mate_index].SecondaryWeapon = item;
                break;
            case EquipSlot.ARMOR:
                TeamMates[team_mate_index].Armor = item;
                break;
            case EquipSlot.CYBERNETICS:
                TeamMates[team_mate_index].Cybernetics = item;
                break;
            case EquipSlot.CONSUMABLE_1:
                TeamMates[team_mate_index].Consumable_1 = item;
                break;
            case EquipSlot.CONSUMABLE_2:
                TeamMates[team_mate_index].Consumable_2 = item;
                break;
            case EquipSlot.SPECIAL_MOD_SLOT:
                TeamMates[team_mate_index].SpecialModSlot = item;
                break;
            default: break;
        }
    }


    public void UnEquipTeamMemberItem(int team_mate_index, SO_InventoryItem item, EquipSlot slot)
    {
        switch (slot)
        {
            case EquipSlot.WEAPON_MAIN:
                TeamMates[team_mate_index].MainWeapon = null;
                break;
            case EquipSlot.WEAPON_SECONDARY:
                TeamMates[team_mate_index].SecondaryWeapon = null;
                break;
            case EquipSlot.ARMOR:
                TeamMates[team_mate_index].Armor = null;
                break;
            case EquipSlot.CYBERNETICS:
                TeamMates[team_mate_index].Cybernetics = null;
                break;
            case EquipSlot.CONSUMABLE_1:
                TeamMates[team_mate_index].Consumable_1 = null;
                break;
            case EquipSlot.CONSUMABLE_2:
                TeamMates[team_mate_index].Consumable_2 = null;
                break;
            case EquipSlot.SPECIAL_MOD_SLOT:
                TeamMates[team_mate_index].SpecialModSlot = null;
                break;
            default:
                break;
        }
    }

    public void EquipARandomEquipment()
    {
        EquipTeamMemberItem(0, Items[0], EquipSlot.WEAPON_MAIN);
    }

    public void UnEquipARandomEquipment()
    {
        UnEquipTeamMemberItem(0, Items[0], EquipSlot.WEAPON_MAIN);
    }
    // size is the same as team members
    [SerializeField] List<CharacterMods> TeamMates;

    [SerializeField] float realKrones;
    [SerializeField] List<SO_InventoryItem> OwnedItems;

    private void Awake()
    {
        //ServiceLocator.Add(this);

    }
    void Start()
    {
        // Testing Only
        TeamMates.Add(GetComponent<LeaderCharacter>().CharacterMods);
        //if (Current == null)
        //    Current = this;
        //if (Current != this)
        //    Destroy(this);


    }


}

[System.Serializable]
public class CharacterMods : IReturnStats
{

    public event Action<SO_InventoryItem> OnAnyModChange;
    public event Action<SO_InventoryItem> OnMainWeaponChange;
    public event Action<SO_InventoryItem> OnSecondaryWeaponChange;
    public event Action<SO_InventoryItem> OnArmorChange;
    public event Action<SO_InventoryItem> OnCyberneticsChange;
    public event Action<SO_InventoryItem> OnSpecialModSlotChange;
    public event Action<SO_InventoryItem> OnConsumable1Change;
    public event Action<SO_InventoryItem> OnConsumable2Change;

    [SerializeField] int teamMateIndex;
    [SerializeField] SO_InventoryItem mainWeapon;
    [SerializeField] SO_InventoryItem secondaryWeapon;
    [SerializeField] SO_InventoryItem armor;
    [SerializeField] SO_InventoryItem cybernetics;
    [SerializeField] SO_InventoryItem consumable_1;
    [SerializeField] SO_InventoryItem consumable_2;
    [SerializeField] SO_InventoryItem specialModSlot;

    [SerializeField] List<SO_InventoryItem> inventoryItems = new List<SO_InventoryItem>();
    [SerializeField] List<SO_InventoryItem> EquipmentSet = new List<SO_InventoryItem>();

    #region Properties
    public int TeamMateIndex
    {
        get { return teamMateIndex; }
        set { teamMateIndex = value; }
    }

    public SO_InventoryItem MainWeapon
    {
        get { return mainWeapon; }
        set
        {
            mainWeapon = value;
            UpdateEquipmentSet();
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnMainWeaponChange?.Invoke(value);
        }
    }

    public SO_InventoryItem SecondaryWeapon
    {
        get { return secondaryWeapon; }
        set
        {
            secondaryWeapon = value;
            UpdateEquipmentSet();
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnSecondaryWeaponChange?.Invoke(value);
        }
    }

    public SO_InventoryItem Armor
    {
        get { return armor; }
        set
        {
            armor = value;
            UpdateEquipmentSet();
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnArmorChange?.Invoke(value);
        }
    }

    public SO_InventoryItem Cybernetics
    {
        get { return cybernetics; }
        set
        {
            cybernetics = value;
            UpdateEquipmentSet();
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnCyberneticsChange?.Invoke(value);
        }
    }

    public SO_InventoryItem SpecialModSlot
    {
        get { return specialModSlot; }
        set
        {
            specialModSlot = value;
            UpdateEquipmentSet();
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnSpecialModSlotChange?.Invoke(value);
        }
    }

    public SO_InventoryItem Consumable_1
    {
        get { return consumable_1; }
        set
        {
            consumable_1 = value;
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnConsumable1Change?.Invoke(value);
        }
    }

    public SO_InventoryItem Consumable_2
    {
        get { return consumable_2; }
        set
        {
            consumable_2 = value;
            UpdateInventoryItems();

            OnAnyModChange?.Invoke(value);
            OnConsumable2Change?.Invoke(value);
        }
    }

    #endregion

    private void UpdateInventoryItems()
    {
        inventoryItems.Clear();

        if (mainWeapon != null)
            inventoryItems.Add(mainWeapon);

        if (secondaryWeapon != null)
            inventoryItems.Add(secondaryWeapon);

        if (armor != null)
            inventoryItems.Add(armor);

        if (cybernetics != null)
            inventoryItems.Add(cybernetics);

        if (consumable_1 != null)
            inventoryItems.Add(consumable_1);

        if (consumable_2 != null)
            inventoryItems.Add(consumable_2);

        if (specialModSlot != null)
            inventoryItems.Add(specialModSlot);
    }

    private void UpdateEquipmentSet()
    {
        EquipmentSet.Clear();

        if (mainWeapon != null)
            EquipmentSet.Add(mainWeapon);

        if (secondaryWeapon != null)
            EquipmentSet.Add(secondaryWeapon);

        if (armor != null)
            EquipmentSet.Add(armor);

        if (cybernetics != null)
            EquipmentSet.Add(cybernetics);

        if (specialModSlot != null)
            EquipmentSet.Add(specialModSlot);
    }

    public Stats_System.CharacterStats ReturnStats()
    {
        UpdateEquipmentSet();

        if (EquipmentSet.Count < 1) return new Stats_System.CharacterStats();

        Stats_System.CharacterStats modStats = new Stats_System.CharacterStats();

        foreach (SO_InventoryItem equipment in EquipmentSet)
            foreach (ItemParameters effect in equipment.ItemEffects)
                modStats.ModifyStat(effect);
        return modStats;
    }
}

public interface IReturnStats
{
    public Stats_System.CharacterStats ReturnStats();
}