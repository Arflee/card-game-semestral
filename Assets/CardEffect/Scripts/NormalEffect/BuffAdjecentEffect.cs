using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff Adjecent", menuName = "Card Effects/Buff Adjecent")]
public class BuffAdjecentEffect : NormalCardEffect
{
    [SerializeField] private int healthBuff = 1;
    [SerializeField] private int damageBuff = 1;

    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        int index = cardOwner.OwnersCardsOnTable.IndexOf(card);
        if (index < 0)
            yield break;

        int upper = index + 1;
        if (upper >= 0 && upper < cardOwner.OwnersCardsOnTable.Count)
        {
            Card adjacent = cardOwner.OwnersCardsOnTable[upper];

            adjacent.BuffHealth(healthBuff);
            adjacent.BuffDamage(damageBuff);
            if (!adjacent.CombatDTO.IsAlive)
            {
                yield return manager.DestroyCard(adjacent);
            }
        }

        int lower = index - 1;
        if (lower >= 0 && lower < cardOwner.OwnersCardsOnTable.Count)
        {
            Card adjacent = cardOwner.OwnersCardsOnTable[lower];

            adjacent.BuffHealth(healthBuff);
            adjacent.BuffDamage(damageBuff);
            if (!adjacent.CombatDTO.IsAlive)
            {
                yield return manager.DestroyCard(adjacent);
            }
        }
    }
}
