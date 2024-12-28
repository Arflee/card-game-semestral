using System.Collections;
using UnityEngine.SceneManagement;

public class LoseState : CombatState
{
    private GameEndingHandler _gameHandler;

    public LoseState(CombatStateMachine machine) : base(machine)
    {
        _gameHandler = machine.GameHandler;
    }

    public override IEnumerator EnterState()
    {
        _gameHandler.PlayerLostGame(SceneManager.GetActiveScene().name);
        
        yield return null;
    }
}
