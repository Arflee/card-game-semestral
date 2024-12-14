using System.Collections;

public class PlayerState : StartingPlayerState
{
    public PlayerState(CombatStateMachine machine) : base(machine)
    {
        StateMachine.OnEndTurn += OnEndTurn;
    }

    public override IEnumerator EnterState()
    {
        StateMachine.PlayerDeck.TakeCard();
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        foreach (var card in StateMachine.PlayerCardsOnTable)
        {
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                effect.OnTurnStart(StateMachine.PlayerDeck, card, StateMachine.PlayerCardsOnTable);
            }
        }

        yield return null;
    }
}
