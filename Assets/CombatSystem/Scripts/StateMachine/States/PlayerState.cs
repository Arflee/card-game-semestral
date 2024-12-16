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
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                // TODO visualize only when effect does something
                // card.CardVisual.ShowEffect();
                // yield return new WaitForSeconds(0.5f);
                effect.OnTurnStart(StateMachine.PlayerOwner, StateMachine, card);
                // card.CardVisual.HideEffect();
            }
        }

        yield return null;
    }
}
