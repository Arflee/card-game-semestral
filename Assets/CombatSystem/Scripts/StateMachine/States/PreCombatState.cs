using System.Collections;
using UnityEngine;

public class PreCombatState : CombatState
{
    private CardDeck _playerDeck;
    private readonly CombatState nextState;

    public PreCombatState(CombatStateMachine machine, CombatState nextState) : base(machine)
    {
        _playerDeck = machine.PlayerDeck;
        this.nextState = nextState;
    }

    public override IEnumerator EnterState()
    {
        StateMachine.SetEndTurnButtonActive(false);
        for (int i = 0; i < _playerDeck.InitialCardsInHand; i++)
        {
            yield return new WaitForSeconds(0.5f);
            _playerDeck.TakeCard(StateMachine.PlayerOwner);
        }

        StateMachine.SetState(nextState);
    }

    public override CombatState NextState()
    {
        return nextState;
    }
}
