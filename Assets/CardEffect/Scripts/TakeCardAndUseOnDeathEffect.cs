using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardEffect", menuName = "Card Effects/On Death/Take card and use")]
public class TakeCardAndUseOnDeathEffect : CardEffect
{
    public override Card OnDeathTakeCardAndUse(CardDeck deck, Card playedCard, List<Card> playerTable)
    {
        var takenCard = deck.TakeCardWithoutAddingToHolder();
        if (takenCard == null)
        {
            Debug.LogWarning("Player is out of cards");
            return null;
        }

        playedCard.Reinitialize(takenCard);
        playedCard.CombatDTO.CardEffects.ForEach(eff => eff.OnUse(deck, playedCard, playerTable));

        return playedCard;
    }
}
