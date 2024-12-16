using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Destroy All Cards", menuName = "Card Effects/Destroy All Cards")]
public class DestroyCardsEffect : NormalCardEffect
{
    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        yield return new WaitForSeconds(0.5f);
        var cards = new List<Card>(cardOwner.OwnersCardsOnTable);
        cards.AddRange(cardOwner.OpponentsCardsOnTable);

        foreach (var c in cards)
        {
            if (c == card)
                continue;
            yield return manager.DestroyCard(c);
        }
    }
}
