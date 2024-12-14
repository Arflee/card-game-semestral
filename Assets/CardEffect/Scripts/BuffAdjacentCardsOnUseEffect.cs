using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Buff adjacent cards")]
public class BuffAdjacentCardsOnUseEffect : CardEffect
{
    [SerializeField] private int healthBuff = 1;
    [SerializeField] private int damageBuff = 1;

    public override void OnUse(CombatState combatState)
    {
        int cardPosition = combatState.OwnersCardsOnTable.Count - 1;

        if (cardPosition >= 1)
        {
            combatState.OwnersCardsOnTable[cardPosition - 1].BuffHealth(healthBuff);
            combatState.OwnersCardsOnTable[cardPosition - 1].BuffDamage(damageBuff);
        }
    }
}
