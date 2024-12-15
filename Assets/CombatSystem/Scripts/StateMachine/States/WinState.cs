using System.Collections;
using System.Collections.Generic;

public class WinState : CombatState
{
    public WinState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        return base.EnterState();
    }
}
