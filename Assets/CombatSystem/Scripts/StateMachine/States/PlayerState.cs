using System.Collections;
using UnityEngine;

public class PlayerState : StartingPlayerState
{
    public PlayerState(CombatStateMachine machine) : base(machine)
    {
        StateMachine.OnEndTurn += OnEndTurn;
    }

    public override IEnumerator EnterState()
    {
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);

        for (int i = 0; i < StateMachine.CardsDrawnPerTurn; i++)
        {
            yield return new WaitForSeconds(0.5f);
            StateMachine.PlayerDeck.TakeCard(StateMachine.PlayerOwner);
        }

        foreach (var card in StateMachine.PlayerCardsOnTable)
        {
            foreach (var effect in card.CombatDTO.OnStartTurnEffects)
            {
                yield return effect.StartEffect(StateMachine, card);
            }

            if (card.TurnPlayed + 2 == StateMachine.CurrentTurn)
            {
                foreach (var effect in card.CombatDTO.AfterTurnEffect)
                {
                    yield return effect.StartEffect(StateMachine, card);
                }
            }
        }

        foreach (var card in StateMachine.EnemyCardsOnTable)
        {
            if (card.TurnPlayed + 2 == StateMachine.CurrentTurn)
            {
                foreach (var effect in card.CombatDTO.AfterTurnEffect)
                {
                    yield return effect.StartEffect(StateMachine, card);
                }
            }
        }

        yield return null;
    }
}
