using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CombatState
{
    private EnemyInitializer _initializer;

    public override CardDeck OwnersDeck => null;

    public override List<Card> OwnersCardsOnTable => StateMachine.EnemyCardsOnTable;

    public override List<Card> OpponentsCardsOnTable => StateMachine.PlayerCardsOnTable;

    public EnemyState(CombatStateMachine machine) : base(machine)
    {
        _initializer = machine.EnemyInitializer;
    }

    public override IEnumerator EnterState()
    {
        foreach (var card in StateMachine.EnemyCardsOnTable)
        {
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                effect.OnTurnStart(this, StateMachine);
            }
        }

        var nextCard = _initializer.GetNextCard();

        if (nextCard == null)
        {
            Debug.LogWarning("Enemy is out of cards!");
            yield return null;
        }
        else
        {
            StateMachine.AddCardOnEnemyTable(nextCard);

            StateMachine.ChangeTurn();
            yield return null;
        }
    }
}
