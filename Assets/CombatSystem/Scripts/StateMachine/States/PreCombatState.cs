using System.Collections;

public class PreCombatState : CombatState
{
    public PreCombatState(CombatStateMachine machine) : base(machine)
    {
    }

    public override IEnumerator EnterState()
    {
        yield return null;

        StateMachine.SetState(new PlayerState(StateMachine));
    }
}
