namespace Stats_System
{
    using System;
    using UnityEngine;

    public struct StatParams
    {
        public StatParams(CharacterStatsType type, float value)
        {
            this.type = type;
            this.value = value;
        }
        public CharacterStatsType type { get; set; }
        public float value { get; set; }
    }


    public class StatsManager : MonoBehaviour, ITakeDamage, ITakeHeal, ILevelUP
    {
        #region Actions\Events
        public event Action OnTakingDamage;
        public event Action OnTakingHeal;
        public event Action<int> OnHealthUpdated;
        #endregion

        [Header("Faction Of The Character")]
        [SerializeField] CharacterFaction faction;

        #region Vars
        [Header("Stats References")]
        [SerializeField] SO_BaseStats baseStats;
        [SerializeField] SO_FactionStats factionStats;

        [Header("Health Vars")]
        [SerializeField] float healthPercentage;
        [SerializeField] int health, maxHealthRef;


        [Header("Base Stats")]
        [SerializeField] private CharacterStats characterBaseStats;
        [Header("Mod Stats")]
        [SerializeField] private CharacterStats characterModStats;
        [Header("Buff/Debuff Stats")]
        [SerializeField] private CharacterStats characterBuffDebuffStats;
        [Header("Overall Stats")]
        [SerializeField] private CharacterStats characterOverallStats;



        #endregion

        #region Properties
        public CharacterFaction Faction => faction;
        public CharacterStats BaseStats => characterBaseStats;
        public CharacterStats ModStats => characterModStats;
        public CharacterStats CharacterBuffDebuffStats => characterBuffDebuffStats;
        public CharacterStats OverallStats => characterOverallStats;


        public int Health
        {
            get => health;
            private set
            {
                health = Mathf.Clamp(value, 0, maxHealthRef);
                OnHealthUpdated?.Invoke(value);

                UpdateHealthPercentage();
            }
        }



        #endregion

        #region Unity
        private void Awake()
        {
            InitializeStats();
        }

        private void OnEnable()
        {
            characterBaseStats.OnAnyStatUpdated += UpdateStats;
            characterModStats.OnAnyStatUpdated += UpdateStats;
            characterBuffDebuffStats.OnAnyStatUpdated += UpdateStats;
        }

        private void OnDisable()
        {
            characterBaseStats.OnAnyStatUpdated -= UpdateStats;
            characterModStats.OnAnyStatUpdated -= UpdateStats;
            characterBuffDebuffStats.OnAnyStatUpdated -= UpdateStats;

        }



        #endregion

        #region Methods

        // Update Stats
        public void UpdateModStats(CharacterStats newStats)
        {
            characterModStats = newStats;
            UpdateStats();
        }

        public void UpdateBuffsStats(CharacterStats newStats)
        {
            characterBuffDebuffStats = newStats;
            UpdateStats();
        }

        private void UpdateStats()
        {
            characterOverallStats = characterBaseStats + characterModStats + characterBuffDebuffStats;
            UpdateMaxHealthRef(characterOverallStats.MaxHP);
            UpdateHealthPercentage();
        }

        private void InitializeStats()
        {
            characterBaseStats = baseStats.Stats + factionStats.Stats;
            UpdateStats();
            maxHealthRef = Mathf.CeilToInt(characterOverallStats.MaxHP);
            Health = maxHealthRef;
        }


        // On Max Health Updated to change the health value to match the maximum HP effect.
        private void UpdateMaxHealthRef(float value)
        {
            if (maxHealthRef > value)
            {
                if (health > value) health = Mathf.CeilToInt(value);
                maxHealthRef = Mathf.CeilToInt(value);
            }
            else if (maxHealthRef < value)
            {
                maxHealthRef = Mathf.CeilToInt(value);
                health = Mathf.CeilToInt(maxHealthRef * healthPercentage);

            }
        }

        private void UpdateHealthPercentage()
        {
            if (maxHealthRef > 0)
                healthPercentage = health / (float)maxHealthRef;
        }

        #endregion

        #region Interfaces
        public void ApplyDamage(StatsManager other, float damageModifier)
        {
            Health -= Mathf.CeilToInt(other.characterModStats.MeleeAttack * damageModifier);
            OnTakingDamage?.Invoke();
        }

        public void ApplyHeal(StatsManager other, float healModifier = 1)
        {
            Health += Mathf.CeilToInt(other.characterModStats.RangedAttack * healModifier);
            OnTakingHeal?.Invoke();
        }

        void ILevelUP.LevelUp()
        {
            if (characterOverallStats.LVL < 10 && characterOverallStats.XP > EXPThreshold())

            {
                characterOverallStats.LVL += 1;
                characterOverallStats.XP -= EXPThreshold();
            }
        }

        public float EXPThreshold()
        {
            return 100;
        }
        #endregion
    }
}