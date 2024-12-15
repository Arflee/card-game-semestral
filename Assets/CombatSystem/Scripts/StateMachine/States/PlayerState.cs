using System.Collections;

public class PlayerState : StartingPlayerState
{
    public PlayerState(CombatStateMachine machine) : base(machine)
    {
        StateMachine.OnEndTurn += OnEndTurn;
    }

    public override IEnumerator EnterState()
    {
        StateMachine.PlayerDeck.TakeCard(StateMachine.PlayerOwner);
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        foreach (var card in StateMachine.PlayerCardsOnTable)
        {
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                effect.OnTurnStart(StateMachine.PlayerOwner, StateMachine, card);
            }
        }

        yield return null;
    }
}
