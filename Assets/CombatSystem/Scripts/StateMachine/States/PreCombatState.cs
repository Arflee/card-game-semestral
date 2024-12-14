using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreCombatState : CombatState
{
    private CardDeck _playerDeck;
    public override CardDeck OwnersDeck => null;

    public override List<Card> OwnersCardsOnTable => null;

    public override List<Card> OpponentsCardsOnTable => null;


    public PreCombatState(CombatStateMachine machine) : base(machine)
    {
        _playerDeck = machine.PlayerDeck;
    }

    public override IEnumerator EnterState()
    {
        for (int i = 0; i < _playerDeck.MaxCardsInHand; i++)
        {
            yield return new WaitForSeconds(0.5f);
            _playerDeck.TakeCard();
        }

        StateMachine.ManaPanel.SpawnCrystalPrefabs(StateMachine.MaxPlayerMana);

        StateMachine.SetState(new StartingPlayerState(StateMachine));
    }
}
