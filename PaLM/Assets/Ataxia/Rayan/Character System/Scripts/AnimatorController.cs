using Stats_System;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator animator;
    [SerializeField] BaseCharacter character;
    [SerializeField] StatsManager characterStats;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        character = GetComponentInChildren<BaseCharacter>();
        characterStats = GetComponentInChildren<StatsManager>();

    }

    private void OnEnable()
    {
        characterStats.OnTakingDamage += PlayGotHitAnimation;
        character.OnCharacterDeath += PlayDeathAnimation;
    }

    private void OnDisable()
    {
        characterStats.OnTakingDamage -= PlayGotHitAnimation;
        character.OnCharacterDeath -= PlayDeathAnimation;

    }

    public void PlayGotHitAnimation()
    {
        animator.SetTrigger("GetHit");
    }

    public void PlayDeathAnimation(BaseCharacter character)
    {
        animator.SetTrigger("Dead");
    }

}
