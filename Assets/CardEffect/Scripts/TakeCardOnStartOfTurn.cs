using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TakeCardOnStartOfTurn", menuName = "Card Effects/On Turn Start/Take card")]
public class TakeCardOnStartOfTurn : CardEffect
{
    public override void OnTurnStart(CardDeck deck, Card card, List<Card> playerTable)
    {
        deck.TakeCard();
    }
}
