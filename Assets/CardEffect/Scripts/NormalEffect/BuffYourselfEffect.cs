using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Yourself", menuName = "Card Effects/Buff Yourself")]
public class BuffYourselfEffect : NormalCardEffect
{
    [SerializeField] private int healthBuff = 1;
    [SerializeField] private int damageBuff = 1;

    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        card.BuffHealth(healthBuff);
        card.BuffDamage(damageBuff);
        if (!card.CombatDTO.IsAlive)
        {
            yield return manager.DestroyCard(card);
        }
    }
}
