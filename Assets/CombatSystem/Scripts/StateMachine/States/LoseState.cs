using System.Collections;

public class LoseState : CombatState
{
    private GameEndingHandler _gameHandler;

    public LoseState(CombatStateMachine machine) : base(machine)
    {
        _gameHandler = machine.GameHandler;
    }

    public override IEnumerator EnterState()
    {
        _gameHandler.PlayerLostGame();
        yield return null;
    }
}
