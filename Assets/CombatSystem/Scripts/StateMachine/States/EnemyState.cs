using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CombatState
{
    private EnemyInitializer _initializer;

    public EnemyState(CombatStateMachine machine) : base(machine)
    {
        _initializer = machine.EnemyInitializer;
    }

    public override IEnumerator EnterState()
    {
        foreach (var card in StateMachine.EnemyCardsOnTable)
        {
            foreach (var effect in card.CombatDTO.OnStartTurnEffects)
            {
                yield return effect.StartEffect(StateMachine, card);
            }
        }

        yield return new WaitForSeconds(0.5f);

        var nextCards = _initializer.GetNextCards(StateMachine.EnemyOwner);
        foreach (var card in nextCards)
        {
            if (card == null)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                StateMachine.AddCardOnEnemyTable(card);
            }
        }

        StateMachine.ChangeTurn();
        yield return null;
    }
}
