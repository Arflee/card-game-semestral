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
        yield return null;
    }
}

