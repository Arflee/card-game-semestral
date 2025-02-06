using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndcreditsPlayerState : CombatState
{
    int cards = 0;

    public EndcreditsPlayerState(CombatStateMachine machine) : base(machine)
    {
    }

    public override void CardAdded(Card card)
    {
        StateMachine.PlayerDeck.TakeCard(StateMachine.PlayerOwner);
        cards++;
        if (cards >= 5)
            StateMachine.SetEndTurnButtonActive(true);
    }

    public override IEnumerator EnterState()
    {
        StateMachine.CurrentTurn++;
        StateMachine.PlayerMana = 10;
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        StateMachine.CanPlayCard = true;
        yield return 0;
    }

    public override CombatState NextState()
    {
        LevelLoader.Instance.LoadScene("Credits");
        return new AttackState(StateMachine);
    }
}
