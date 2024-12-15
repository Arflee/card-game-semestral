using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Buff adjacent cards")]
public class BuffAdjacentCardsOnUseEffect : CardEffect
{
    [SerializeField] private int healthBuff = 1;
    [SerializeField] private int damageBuff = 1;

    public override void OnUse(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        int index = cardOwner.OwnersCardsOnTable.Count - 2;
        if (index < 0)
            return;

        Card adjacent = cardOwner.OwnersCardsOnTable[index];

        adjacent.BuffHealth(healthBuff);
        adjacent.BuffDamage(damageBuff);
        if (!adjacent.CombatDTO.IsAlive)
        {
            manager.DestroyCard(adjacent);
        }
    }
}
