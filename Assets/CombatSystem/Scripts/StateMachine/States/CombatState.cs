using System;
using System.Collections;

public abstract class CombatState
{
    protected CombatStateMachine StateMachine { get; }

    public CombatState(CombatStateMachine machine)
    {
        StateMachine = machine;
    }

    public virtual IEnumerator EnterState()
    {
        throw new NotImplementedException();
    }
}
