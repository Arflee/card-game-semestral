using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Buff adjacent cards")]
public class BuffAdjacentCardsOnUseEffect : CardEffect
{
    [SerializeField] private int healthBuff = 1;
    [SerializeField] private int damageBuff = 1;

    public override void OnUse(CombatState combatState, CombatStateMachine manager, Card card)
    {
        card.BuffHealth(healthBuff);
        card.BuffDamage(damageBuff);
    }
}
