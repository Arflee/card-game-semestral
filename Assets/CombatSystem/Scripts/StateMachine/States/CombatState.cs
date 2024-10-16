using System;
using System.Collections;
using System.Collections.Generic;

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

    public virtual IEnumerator Attack(CombatSlot slot)
    {
        throw new NotImplementedException();
    }

    public virtual IEnumerator EndTurn(DragNDropTable table)
    {
        throw new NotImplementedException();
    }
}
