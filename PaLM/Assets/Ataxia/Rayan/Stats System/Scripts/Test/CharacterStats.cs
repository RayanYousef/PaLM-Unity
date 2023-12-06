using System;
using UnityEngine;

namespace Stats_System
{
    public enum CharacterStatsType
    {
        None,
        XP,
        MOVEMENT,
        TEAM_SLOTS,
        MAXHP,
        MELEE_ATTACK,
        MELEE_ACCURACY,
        RANGED_ATTACK,
        RANGED_ACCURACY,
        CRITICAL_HIT_CHANCE,
        MELEE_DEFENCE,
        RANGED_DEFENCE,
        SPEED,
        HACKING_SUCCESS_RATE,
        HACKING_STRENGTH,
        CYBER_SECURITY,
        SUPPORT,
        STEALTH,
        DETECT,
        INFLUENCE,
        BUSINESS,
    }

    [Serializable]
    public struct CharacterStats
    {
        public event Action OnAnyStatUpdated;














        #region Old props

        //private void AddModifier(CharacterStatsType name)
        //{
        //    characterStatsDict.Find(x => x.Name == name).Stat.AddModifier("test", 0);
        //}

        [Header("Character Stats")]
        [SerializeField] private int m_movement;
        public int Movement
        {
            get { return m_movement; }
            set
            {
                m_movement = Mathf.Clamp(value, 0, int.MaxValue);
                OnMovementUpdated?.Invoke(m_movement); // Invoke the event
                OnAnyStatUpdated?.Invoke(); // Invoke the common event

            }
        }
        public event Action<int> OnMovementUpdated; // Event for Movement

        [SerializeField] private int m_teamSlots;
        public int TeamSlots
        {
            get { return m_teamSlots; }
            set
            {
                m_teamSlots = Mathf.Clamp(value, 0, int.MaxValue);
                OnTeamSlotsUpdated?.Invoke(m_teamSlots); // Invoke the event
                OnAnyStatUpdated?.Invoke(); // Invoke the common event

            }
        }
        public event Action<int> OnTeamSlotsUpdated; // Event for TeamSlots

        [SerializeField] private float m_xp;
        public float XP
        {
            get { return m_xp; }
            set
            {
                m_xp = Mathf.Clamp(value, 0, float.MaxValue);
                OnXPUpdated?.Invoke(m_xp); // Invoke the event
                OnAnyStatUpdated?.Invoke(); // Invoke the common event

            }
        }
        public event Action<float> OnXPUpdated; // Event for XP

        [SerializeField] private int m_lvl;
        public int LVL
        {
            get { return m_lvl; }
            set
            {
                m_lvl = Mathf.Clamp(value, 0, int.MaxValue);
                OnLvlUpdated?.Invoke(m_lvl); // Invoke the event
                OnAnyStatUpdated?.Invoke(); // Invoke the common event

            }
        }
        public event Action<int> OnLvlUpdated; // Event for Lvl



        [Header("Combat Stats")]
        /// <summary>
        /// Max Points
        /// </summary>
        [SerializeField] private float m_maxHP;
        public float MaxHP
        {
            get { return m_maxHP; }
            set
            {
                m_maxHP = Mathf.Clamp(value, 0, float.MaxValue);
                OnMaxHealthUpdated?.Invoke(m_maxHP);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event

            }
        }
        public event Action<float> OnMaxHealthUpdated;

        /// <summary>
        /// melee damage
        /// </summary>
        [SerializeField] private float m_melee_attack;
        public float MeleeAttack
        {
            get { return m_melee_attack; }
            set
            {
                m_melee_attack = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// melee accuracy
        /// </summary>
        [SerializeField] private float m_melee_accuracy;
        public float MeleeAccuracy
        {
            get { return m_melee_accuracy; }
            set
            {
                m_melee_accuracy = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// ranged damage
        /// </summary>
        [SerializeField] private float m_ranged_attack;
        public float RangedAttack
        {
            get { return m_ranged_attack; }
            set
            {
                m_ranged_attack = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// ranged accuracy
        /// </summary>
        [SerializeField] private float m_ranged_accuracy;
        public float RangedAccuracy
        {
            get { return m_ranged_accuracy; }
            set
            {
                m_ranged_accuracy = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// critical hit % (0->1)(melee or ranged)
        /// </summary>
        [SerializeField] private float m_critical_hit_chance;
        public float CriticalHitChance
        {
            get { return m_critical_hit_chance; }
            set
            {
                m_critical_hit_chance = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// melee defense (0->1) (%  of reduction of incoming damage)
        /// </summary>
        [SerializeField] private float m_melee_defense;
        public float MeleeDefense
        {
            get { return m_melee_defense; }
            set
            {
                m_melee_defense = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// ranged defense (0->1) (% of reduction of incoming damage)
        /// </summary>
        [SerializeField] private float m_ranged_defense;
        public float RangedDefense
        {
            get { return m_ranged_defense; }
            set
            {
                m_ranged_defense = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// speed (initiative on what order unit takes action in combat - if equal then attacker has first attack initiative)
        /// </summary>
        [SerializeField] private float m_speed;
        public float Speed
        {
            get { return m_speed; }
            set
            {
                m_speed = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// hacking success rate (0->1)(% accuracy )
        /// </summary>
        [SerializeField] private float m_hacking_success_rate;
        public float HackingSuccessRate
        {
            get { return m_hacking_success_rate; }
            set
            {
                m_hacking_success_rate = Mathf.Clamp01(value);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// hacking strength (will make check against hacking defense of enemy unit or team)
        /// </summary>
        [SerializeField] private float m_hacking_strength;
        public float HackingStrength
        {
            get { return m_hacking_strength; }
            set
            {
                m_hacking_strength = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// hacking defense (will make check against hacking strength of enemy unit)
        /// </summary>
        [SerializeField] private float m_cyber_security;
        public float Cybersecurity
        {
            get { return m_cyber_security; }
            set
            {
                m_cyber_security = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// support (modifies efficacy of a support action)
        /// </summary>
        [SerializeField] private float m_support;
        public float Support
        {
            get { return m_support; }
            set
            {
                m_support = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// stealth rating % (0->1)(for determining if hero/team will enter a sector undetected)
        /// </summary>
        [SerializeField] private float m_stealth;
        public float Stealth
        {
            get { return m_stealth; }
            set
            {
                m_stealth = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// detection rating % (0->1)(for detecting stealth heroes/teams in a sector)
        /// </summary>
        [SerializeField] private float m_detect;
        public float Detect
        {
            get { return m_detect; }
            set
            {
                m_detect = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// influence rating % (0->1)(for determining several social outcomes such as success of police bribery in a sector)
        /// </summary>
        [SerializeField] private float m_influence;
        public float Influence
        {
            get { return m_influence; }
            set
            {
                m_influence = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }

        /// <summary>
        /// Base business rating % (0->1)(for determining several economic outcomes such as building/item purchase price & income from extortion or a successfully setup illegal operation)
        /// </summary>
        [SerializeField] private float m_business;
        public float Business
        {
            get { return m_business; }
            set
            {
                m_business = Mathf.Clamp(value, 0, float.MaxValue);
                OnAnyStatUpdated?.Invoke(); // Invoke the common event
            }
        }
        #endregion

        #region Methods
        public void ModifyStat(ItemParameters param)
        {

            switch (param.StatType)
            {
                case CharacterStatsType.MAXHP:
                    this.MaxHP = this.MaxHP + param.Value;
                    break;
                case CharacterStatsType.XP:
                    this.XP += param.Value;
                    break;

                case CharacterStatsType.MOVEMENT:
                    this.Movement += Mathf.FloorToInt(param.Value);
                    break;

                case CharacterStatsType.TEAM_SLOTS:
                    this.TeamSlots += Mathf.FloorToInt(param.Value);
                    break;

                case CharacterStatsType.MELEE_ATTACK:
                    this.MeleeAttack += param.Value;
                    break;

                case CharacterStatsType.MELEE_ACCURACY:
                    this.MeleeAccuracy += param.Value;
                    break;

                case CharacterStatsType.RANGED_ATTACK:
                    this.RangedAttack += param.Value;
                    break;

                case CharacterStatsType.RANGED_ACCURACY:
                    this.RangedAccuracy += param.Value;
                    break;

                case CharacterStatsType.CRITICAL_HIT_CHANCE:
                    this.CriticalHitChance += param.Value;
                    break;

                case CharacterStatsType.MELEE_DEFENCE:
                    this.MeleeDefense += param.Value;
                    break;

                case CharacterStatsType.RANGED_DEFENCE:
                    this.RangedDefense += param.Value;
                    break;

                case CharacterStatsType.SPEED:
                    this.Speed += param.Value;
                    break;

                case CharacterStatsType.HACKING_SUCCESS_RATE:
                    this.HackingSuccessRate += param.Value;
                    break;

                case CharacterStatsType.HACKING_STRENGTH:
                    this.HackingStrength += param.Value;
                    break;

                case CharacterStatsType.CYBER_SECURITY:
                    this.Cybersecurity += param.Value;
                    break;

                case CharacterStatsType.SUPPORT:
                    this.Support += param.Value;
                    break;

                case CharacterStatsType.STEALTH:
                    this.Stealth += param.Value;
                    break;

                case CharacterStatsType.DETECT:
                    this.Detect += param.Value;
                    break;

                case CharacterStatsType.INFLUENCE:
                    this.Influence += param.Value;
                    break;

                case CharacterStatsType.BUSINESS:
                    this.Business += param.Value;
                    break;
                default:
                    break;
            }

        }

        public void Clear()
        {
            Movement = 0;
            TeamSlots = 0;
            XP = 0;
            LVL = 0;
            MaxHP = 0;
            MeleeAttack = 0;
            MeleeAccuracy = 0;
            RangedAttack = 0;
            RangedAccuracy = 0;
            CriticalHitChance = 0;
            MeleeDefense = 0;
            RangedDefense = 0;
            Speed = 0;
            HackingSuccessRate = 0;
            HackingStrength = 0;
            Cybersecurity = 0;
            Support = 0;
            Stealth = 0;
            Detect = 0;
            Influence = 0;
            Business = 0;
        }
        #endregion

        #region Operator Overload
        public static CharacterStats operator +(CharacterStats a, CharacterStats b)
        {
            return new CharacterStats
            {

                Movement = a.Movement + b.Movement,
                TeamSlots = a.TeamSlots + b.TeamSlots,
                XP = a.XP + b.XP,
                LVL = a.LVL + b.LVL,

                MaxHP = a.MaxHP + b.MaxHP,
                MeleeAttack = a.MeleeAttack + b.MeleeAttack,
                MeleeAccuracy = a.MeleeAccuracy + b.MeleeAccuracy,
                RangedAttack = a.RangedAttack + b.RangedAttack,
                RangedAccuracy = a.RangedAccuracy + b.RangedAccuracy,
                CriticalHitChance = a.CriticalHitChance + b.CriticalHitChance,
                MeleeDefense = a.MeleeDefense + b.MeleeDefense,
                RangedDefense = a.RangedDefense + b.RangedDefense,
                Speed = a.Speed + b.Speed,
                HackingSuccessRate = a.HackingSuccessRate + b.HackingSuccessRate,
                HackingStrength = a.HackingStrength + b.HackingStrength,
                Cybersecurity = a.Cybersecurity + b.Cybersecurity,
                Support = a.Support + b.Support,
                Stealth = a.Stealth + b.Stealth,
                Detect = a.Detect + b.Detect,
                Influence = a.Influence + b.Influence,
                Business = a.Business + b.Business
            };
        }
        public static CharacterStats operator -(CharacterStats a, CharacterStats b)
        {
            return new CharacterStats
            {
                Movement = a.Movement - b.Movement,
                TeamSlots = a.TeamSlots - b.TeamSlots,
                XP = a.XP - b.XP,
                LVL = a.LVL - b.LVL,

                MaxHP = a.MaxHP - b.MaxHP,
                MeleeAttack = a.MeleeAttack - b.MeleeAttack,
                MeleeAccuracy = a.MeleeAccuracy - b.MeleeAccuracy,
                RangedAttack = a.RangedAttack - b.RangedAttack,
                RangedAccuracy = a.RangedAccuracy - b.RangedAccuracy,
                CriticalHitChance = a.CriticalHitChance - b.CriticalHitChance,
                MeleeDefense = a.MeleeDefense - b.MeleeDefense,
                RangedDefense = a.RangedDefense - b.RangedDefense,
                Speed = a.Speed - b.Speed,
                HackingSuccessRate = a.HackingSuccessRate - b.HackingSuccessRate,
                HackingStrength = a.HackingStrength - b.HackingStrength,
                Cybersecurity = a.Cybersecurity - b.Cybersecurity,
                Support = a.Support - b.Support,
                Stealth = a.Stealth - b.Stealth,
                Detect = a.Detect - b.Detect,
                Influence = a.Influence - b.Influence,
                Business = a.Business - b.Business
            };
        }
        #endregion
    }
}