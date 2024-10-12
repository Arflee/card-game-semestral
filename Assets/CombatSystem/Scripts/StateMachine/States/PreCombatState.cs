using System.Collections;
using UnityEngine;

public class PreCombatState : CombatState
{
    private CardDeck _playerDeck;

    public PreCombatState(CombatStateMachine machine) : base(machine)
    {
        _playerDeck = machine.PlayerDeck;
    }

    public override IEnumerator EnterState()
    {
        for (int i = 0; i < _playerDeck.MaxCardsInHand; i++)
        {
            _playerDeck.TakeCard();
            yield return new WaitForSeconds(0.5f);
        }

        //StateMachine.SetState(new PlayerState(StateMachine));
    }
}
