using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TakeCardOnStartOfTurn", menuName = "Card Effects/On Turn Start/Take card")]
public class TakeCardOnStartOfTurn : CardEffect
{
    [SerializeField, Min(1)] private int cardsAmount;

    public override void OnTurnStart(CombatState combatState, CombatStateMachine manager)
    {
        if (combatState.OwnersDeck == null)
            return;

        for (int i = 0; i < cardsAmount; i++)
        {
            if (combatState.OwnersDeck.TakeCard() == null)
            {
                Debug.Log("ran out of cards in deck");
            }
        }
    }
}
