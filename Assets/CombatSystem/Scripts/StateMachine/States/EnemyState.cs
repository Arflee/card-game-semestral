using System.Collections;

public class EnemyState : CombatState
{
    public EnemyState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        return base.EnterState();
    }
}
