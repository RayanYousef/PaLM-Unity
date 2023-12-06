
using Stats_System;
using System;
using UnityEngine;

public enum CharacterFaction { None, SobekiCorp, Faction2, Faction3,Faction4}

public class BaseCharacter : MonoBehaviour

{
    #region Events
    public Action<BaseCharacter> OnCharacterDeath;
    #endregion

    #region Fields
    [Header("Components")]
    //[SerializeField] ProgressBar healthBar;
    [SerializeField] StatsManager characterStats;

    public StatsManager CharacterStats => characterStats;
    #endregion

    private void Awake()
    {
        // Initialize components
        characterStats = GetComponentInChildren<StatsManager>();
        //healthBar = GetComponentInChildren<ProgressBar>();

    }

    public virtual void OnEnable()
    {

        //characterStats.Current_Stats.OnHealthUpdated += OnCharacterHealthUpdated;
    }

    public virtual void OnDisable()
    {
        //characterStats.Current_Stats.OnHealthUpdated -= OnCharacterHealthUpdated;
    }

    private void OnCharacterHealthUpdated(float health)
    {
        //healthBar.maxValue = characterStats.OverallStats.MaxHP;
        //healthBar.SetValue(health);

        if (health <= 0)
            OnCharacterDeath?.Invoke(this);
    }
}
