using System.Collections;
using UnityEngine;

public class StartingPlayerState : CombatState
{
    public StartingPlayerState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        StateMachine.ManaPanel.ResetManaCrystals(StateMachine.PlayerMana);
        yield return null;
        StateMachine.ChangeTurn();
    }
}
