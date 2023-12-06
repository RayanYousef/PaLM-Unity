using Stats_System;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[System.Serializable]
public class ItemParameters
{
    public CharacterStatsType StatType;
    public float Value;
}

[CreateAssetMenu(menuName ="Inventory/InventoryItem")]
public class SO_InventoryItem : ScriptableObject
{
    public string ItemName { get { return this.Name; } }
    public string ItemDescription { get { return Description; } }
    public Sprite ItemIcon { get { return Icon; } }
    public float ItemPrice { get { return this.Price; } }
    public float ItemRetailPrice { get { return this.RetailPrice; } }
    public Type ItemType { get { return this._Type; } }
    public GameObject ItemPrefab { get { return this.Prefab; } }
    public User ItemAllowedUser { get { return this.user; } }
    public List<ItemParameters> ItemEffects { get { return this.itemEffects; } }

    [SerializeField] Sprite Icon;
    [SerializeField] GameObject Prefab;

    [Header("Basic Info")]
    [SerializeField] string Name; 
    [SerializeField] string Description;
    [SerializeField] float Price;
    [SerializeField] float RetailPrice;
    [SerializeField] Type _Type;
    [SerializeField] User user;


    
    [Space(10)]
    [Header("The Effects it has on the Stats")]
    [SerializeField] List<ItemParameters> itemEffects;

    public enum Type
    {
        WEAPON, SECONDARY, CONSUMABLE_1,CONSUMABLE_2, ARMOR, CYBERNETICS, SPECIALMOD
    }

    public enum User 
    {
        None = 0,
        SOBEKI_CORP = 1 << 1,
        CORAL_HEIGHTS_BRUTES = 1 << 2 ,
        THE_GENESIS = 1<< 3,
    }
}




