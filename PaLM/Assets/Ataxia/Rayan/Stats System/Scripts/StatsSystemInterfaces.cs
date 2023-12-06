namespace Stats_System
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ITakeDamage
    {
        public event Action OnTakingDamage;

        public void ApplyDamage(StatsManager other, float damageModifier = 1);
    }

    public interface ITakeHeal
    {
        public event Action OnTakingHeal;

        public void ApplyHeal(StatsManager other, float healModifier = 1);
    }

    public interface ILevelUP
    {

        public float EXPThreshold();
        public void LevelUp();

    }

}