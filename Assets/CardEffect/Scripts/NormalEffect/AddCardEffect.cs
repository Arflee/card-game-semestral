using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Add Card", menuName = "Card Effects/Add Card")]
public class AddCardEffect : NormalCardEffect
{
    [SerializeField, Min(1)] private int cardsAmount;

    protected override IEnumerator TriggerEffect(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        if (cardOwner.OwnersDeck == null)
        {
            yield return new WaitForSeconds(0.5f);
            yield break;
        }

        for (int i = 0; i < cardsAmount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            if (cardOwner.OwnersDeck.TakeCard(card.Owner) == null)
            {
                Debug.Log("ran out of cards in deck");
            }
        }
    }
}
