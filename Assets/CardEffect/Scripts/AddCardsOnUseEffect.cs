using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Add cards to player hand on play")]
public class AddCardsOnUseEffect : CardEffect
{
    [SerializeField, Min(1)] private int cardsAmount;

    public override void OnUse(CardOwner cardOwner, CombatStateMachine manager, Card card)
    {
        if (cardOwner.OwnersDeck == null)
            return;

        for (int i = 0; i < cardsAmount; i++)
        {
            if (cardOwner.OwnersDeck.TakeCard(card.Owner) == null)
            {
                Debug.Log("ran out of cards in deck");
            }
        }
    }
}
