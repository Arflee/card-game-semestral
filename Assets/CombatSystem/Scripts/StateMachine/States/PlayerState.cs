using System.Collections;

public class PlayerState : StartingPlayerState
{
    private CardOwner _owner;

    public PlayerState(CombatStateMachine machine) : base(machine)
    {
        StateMachine.OnEndTurn += OnEndTurn;
        _owner = new CardOwner(this);
    }

    public override IEnumerator EnterState()
    {
        StateMachine.PlayerDeck.TakeCard(_owner);
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        foreach (var card in StateMachine.PlayerCardsOnTable)
        {
            foreach (var effect in card.CombatDTO.CardEffects)
            {
                effect.OnTurnStart(this, StateMachine, card);
            }
        }

        yield return null;
    }
}
