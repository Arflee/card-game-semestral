using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Use/Transform all own cards")]
public class TransformAllOwnCardsToOtherEffect : CardEffect
{
    [SerializeField] private CombatCard changeCard;

    public override void OnUse(CardDeck deck, Card usedCard, List<Card> playerTable)
    {
        foreach (var card in playerTable)
        {
            card.Reinitialize(changeCard);
        }
    }
}
