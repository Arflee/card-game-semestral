using System.Collections;

public class PlayerState : CombatState
{
    public PlayerState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        return base.EnterState();
    }

    public override IEnumerator Attack()
    {
        return base.Attack();
    }
}
