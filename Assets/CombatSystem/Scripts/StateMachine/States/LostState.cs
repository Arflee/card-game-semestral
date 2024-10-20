using System.Collections;

public class LostState : CombatState
{
    public LostState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        return base.EnterState();
    }
}
